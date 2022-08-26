using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityDebugger : MonoBehaviour
{
    [SerializeField]
    private float maxVelocity = 20f;

    public float force = 20f;
    private void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, force, 0) * 10);
    }
    private void Update()
    {
        GetComponent<Renderer>().material.color = ColorForVelocity();
    }

    private Color ColorForVelocity()
    {
        float velocity = GetComponent<Rigidbody>().velocity.magnitude;

        return Color.Lerp(Color.green, Color.red, velocity / maxVelocity);
    }
}
