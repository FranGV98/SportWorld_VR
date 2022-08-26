using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannons_Manager : MonoBehaviour
{
    public Cannon_Bhv RedCannon;
    public Cannon_Bhv BlueCannon;

    private float timer;
    public float cadence;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > cadence)
        {
            int random = Random.Range(0, 2);
            print(random);
            if(random == 0)
            {
                RedCannon.MoveTarget();
                RedCannon.ShootBall();
            }
            else
            {
                BlueCannon.MoveTarget();
                BlueCannon.ShootBall();
            }

            timer = 0;
        }
    }
}
