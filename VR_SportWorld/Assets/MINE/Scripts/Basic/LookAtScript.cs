using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, target.transform.position) > 5)
        transform.LookAt(target);
    }
}
