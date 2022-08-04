using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelSpin : MonoBehaviour
{
    public float RotSpeed = 100;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * RotSpeed);

        SuperSpin();
    }

    public void SuperSpin()
    {
        if(RotSpeed > 100)
        {
            RotSpeed -= 200 * Time.deltaTime;
        }
    }
}
