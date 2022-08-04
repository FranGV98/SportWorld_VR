using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Player_Blocks : MonoBehaviour
{
    private int int_score;
    private float fl_timer;
    public Text text_score, text_state, text_timer;
    public GameObject StartScreen;
    private BlockManager _blockManager;

    // Start is called before the first frame update
    void Start()
    {
        _blockManager = GameObject.Find("Blocks_Manager").GetComponent<BlockManager>();
        //Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        fl_timer += Time.deltaTime;
        text_timer.text = "Time: " + fl_timer;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Damagable"))
        {
            print("Muerto");
            text_state.text = "State: Dead";
        }

        if (col.gameObject.layer == LayerMask.NameToLayer("Scorer"))
        {
            int_score += 100;
            print("Score = " + int_score);
            text_score.text = "Score: " + int_score;
            text_state.text = "State: Alive";

            if(col.tag == "Up")
            {
                _blockManager.int_UpScorerCount++;
                _blockManager.int_DownScorerCount = 0;
            }

            if (col.tag == "Down")
            {
                _blockManager.int_DownScorerCount++;
                _blockManager.int_UpScorerCount = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Damagable"))
        {
            Destroy(other.gameObject);
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Scorer"))
        {
            _blockManager.ColorTransition();

            Destroy(other.transform.parent.gameObject);

        }
    }
}
