using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingManager : MonoBehaviour
{

    public GameObject go_haloPrefab;
    public List<GameObject> halosList;
    
    private int num_Halos;
    public int num_leftHalos;

    public float time_countdown;
    public Text text_countdown;

    // Start is called before the first frame update
    void Start()
    {
        time_countdown = 30;
        num_Halos = 5;
        num_leftHalos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time_countdown -= Time.deltaTime;
        DisplayTime(time_countdown);

        if(num_leftHalos <= 0)
        {
            CreateRound();
        }
    }

    void CreateRound()
    {
        halosList.Clear();
        for(int i = 0; i<num_Halos; i++)
        {
            GameObject newHalo =
            GameObject.Instantiate(go_haloPrefab, new Vector3(Random.Range(-119, 74), Random.Range(3f, 12f), Random.Range(-100, 96)), 
            Quaternion.Euler(new Vector3(90, Random.Range(0f,360f), go_haloPrefab.transform.rotation.z)));

            halosList.Add(newHalo);
            num_leftHalos++;
        }
        num_Halos = num_leftHalos;
        
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        text_countdown.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
