using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class RacketPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject RedRacket, BlueRacket;
    public GameObject LeftHand, RightHand;
    public float speed = 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.Start))
        {
            Debug.Log("Sida0");
            Vector3 handPos = RightHand.transform.position;
            BlueRacket.transform.position = Vector3.MoveTowards(transform.position, handPos, speed * Time.deltaTime);
        }
    }
}
