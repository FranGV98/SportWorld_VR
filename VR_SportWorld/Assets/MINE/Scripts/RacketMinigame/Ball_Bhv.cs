using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Bhv : MonoBehaviour
{
    float lifetimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetimer += Time.deltaTime;

        if(lifetimer > 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Finish")
        {
            Destroy(gameObject);
        }
    }
}
