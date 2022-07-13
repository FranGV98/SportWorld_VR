using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Creator : MonoBehaviour
{
    public float Bullet_Force = 4000;
    public GameObject ball_prefab;
    public GameObject player;
    private float timer;
    public float cadence;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        cadence = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > cadence)
        {
            GameObject newBullet = Instantiate(ball_prefab, transform.position, transform.rotation) as GameObject;

            newBullet.transform.LookAt(player.transform);

            Rigidbody newRigidBody = newBullet.GetComponent<Rigidbody>();
            newRigidBody.AddForce(transform.forward * Bullet_Force);
           
            timer = 0;
        }
    }
}
