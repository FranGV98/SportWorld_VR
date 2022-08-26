using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Cannon_Bhv : MonoBehaviour
{
    public float ball_Speed = 600;
    public GameObject ball_prefab;
    public GameObject go_turret;

    //Target
    public GameObject go_target;
    public Vector2 X_Limits, Y_Limits;

    public void ShootBall()
    {
        go_turret.transform.LookAt(go_target.transform);

        float X_Angle = CalculateShotAngle(ball_Speed, go_target.transform.position);

        if (float.IsNaN(Math.Abs(X_Angle)))
        {
            print("Target out of range");
            return;
        }

        go_turret.transform.Rotate(X_Angle, 0, 0);

        GameObject newBullet = Instantiate(ball_prefab, transform.position, transform.rotation) as GameObject;
        Rigidbody newRigidBody = newBullet.GetComponent<Rigidbody>();
        newRigidBody.velocity = transform.forward * ball_Speed;
    }

    public void MoveTarget()
    {
        go_target.transform.position = new Vector3(UnityEngine.Random.Range(X_Limits.x, X_Limits.y), UnityEngine.Random.Range(Y_Limits.x, Y_Limits.y), go_target.transform.position.z);
    }

    float CalculateShotAngle(float _ballSpeed, Vector3 _target) //Calculations are correct
    {
        float distance = Vector3.Distance(transform.position, _target);

        float parenthesis = Physics.gravity.y * distance * distance; //g * x^2

        double numerator = Math.Sqrt(Math.Pow(_ballSpeed, 4) - (Physics.gravity.y * parenthesis)); //v^4 - g * (g*x^2)

        double ATangle = ((Math.Pow(_ballSpeed, 2)) - numerator) / (Physics.gravity.y * distance); //

        double angle = Math.Atan(ATangle);

        float result = (float)angle * Mathf.Rad2Deg;

        return result;
    }
}
