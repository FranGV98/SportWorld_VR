using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PanelsFollower : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Camera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, Camera.transform.rotation.eulerAngles.y, transform.rotation.z));
    }
}
