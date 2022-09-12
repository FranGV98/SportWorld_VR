using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public List<GameObject> go_list_blockPrefabs, go_list_Tunnels;
    public List<GameObject> go_list_tempblocks;
    public List<Material> mat_list_blockMats, mat_list_TunnelMats;
    public GameObject go_playerHead;
    public float fl_playerHeigth;
    private float fl_spawntimer, fl_spawntime;
    private int int_wave, int_difficulty, int_phase, int_spawn;
     public int int_UpScorerCount, int_DownScorerCount;
    [HideInInspector] public int int_current_color;

    // Start is called before the first frame update
    void Start()
    {
        fl_spawntime = 3;
        fl_spawntimer = fl_spawntime;

        //fl_playerHeigth = go_playerHead.transform.position.y;
        fl_playerHeigth = 1.8f;
    }

    // Update is called once per frame
    void Update()
    {
        SimpleSpawnSystem();
    }

    void SimpleSpawnSystem()
    {      
        fl_spawntimer -= Time.deltaTime;

        float fl_SpawnPos = Random.Range(0.3f, fl_playerHeigth);

        if (int_UpScorerCount >= 2 || int_DownScorerCount >= 2)  //Avoid having a lot of obstacles in the same heigth
        {
            fl_SpawnPos = go_playerHead.transform.position.y + 0.15f;
            int_UpScorerCount = 0;
            int_DownScorerCount = 0;
        }

        if (fl_spawntimer < 0)
        {
            if (int_current_color > 3)
            { int_current_color = 0; }

            GameObject current_Block =
            GameObject.Instantiate(go_list_blockPrefabs[0], new Vector3(transform.position.x, fl_SpawnPos, transform.position.z), transform.rotation);

            current_Block.transform.localScale = new Vector3(current_Block.transform.localScale.x, Random.Range(0.6f, 1f), Random.Range(0.6f, 2f));

            go_list_tempblocks.Add(current_Block);

            //fl_spawntime -= 0.1f;
            fl_spawntimer = fl_spawntime;
        }
    }

    public void ColorTransition()
    {
        go_list_Tunnels[0].GetComponent<TunnelSpin>().RotSpeed = 500;

        int randomMat_int = (int)Random.Range(0, mat_list_blockMats.Count);
        go_list_blockPrefabs[0].GetComponent<Renderer>().material = mat_list_blockMats[int_current_color];
        go_list_Tunnels[0].GetComponent<Renderer>().material = mat_list_blockMats[int_current_color];
        go_list_Tunnels[2].GetComponent<Renderer>().material = mat_list_blockMats[3-int_current_color];
        go_list_Tunnels[1].GetComponent<Renderer>().material = mat_list_TunnelMats[3-int_current_color];

        for (int i = 0; i < go_list_tempblocks.Count; i++)
        {
            if (go_list_tempblocks[i] == null)
            {
                go_list_tempblocks.Remove(go_list_tempblocks[i]);
            }
            else
            {
                go_list_tempblocks[i].GetComponent<Renderer>().material = mat_list_blockMats[int_current_color];
            }
        }

        int_current_color++;
    }
}
