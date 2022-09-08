using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingPlayer : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public Text R_Force, verticalForceT;
    public GameObject go_Camera;

    public GameObject go_R_Hand, go_L_Hand;
    public float WingForce;
    Vector3 v3_R_LastPos, v3_L_LastPos;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = transform.GetComponent<Rigidbody>();

        v3_R_LastPos = go_R_Hand.transform.position - transform.position;
        v3_L_LastPos = go_L_Hand.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate relative Speed of both hands respect the player position
        var RHand_Speed = ((go_R_Hand.transform.position - transform.position) - v3_R_LastPos) / Time.deltaTime;
        v3_R_LastPos = go_R_Hand.transform.position - transform.position;

        var LHand_Speed = ((go_L_Hand.transform.position - transform.position) - v3_L_LastPos) / Time.deltaTime;
        v3_L_LastPos = go_L_Hand.transform.position - transform.position;

        
        float verticalForce = RHand_Speed.y + LHand_Speed.y;
        verticalForceT.text = "VSpeed: " + (int)verticalForce;

        if (verticalForce < -1f && verticalForce > -16f & transform.position.y <= 15f)
        {
            Vector3 YForce = new Vector3(0, verticalForce * -WingForce);
            Vector3 FlightForce = go_Camera.transform.forward * Mathf.Abs(verticalForce * 6) + YForce;
            _rigidbody.AddForce(FlightForce);
        }

        if(_rigidbody.velocity.y <= -3.5f)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, -3.5f, _rigidbody.velocity.z);
        }
        if (_rigidbody.velocity.y >= 5f)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 5, _rigidbody.velocity.z);
        }

        var HorRHand_Speed = new Vector3(RHand_Speed.x, 0, RHand_Speed.z);
        var HorLHand_Speed = new Vector3(LHand_Speed.x, 0, LHand_Speed.z);
        var HorizontalForce = HorRHand_Speed + HorLHand_Speed;

        if (HorizontalForce.magnitude > 5)
        {
            var force = HorizontalForce.magnitude * -5f;
            Vector3 FlightHorForce = go_Camera.transform.forward * force;
            _rigidbody.AddForce(FlightHorForce);
        }

        R_Force.text = "HorSpeed: " + (int)HorizontalForce.magnitude;
        //if(_rigidbody.velocity.y > )

        //if (transform.position.y > 0)
        //{
        //    //_rigidbody.AddForce(Camera.transform.forward * Mathf.Abs(_rigidbody.velocity.y * 3);

        //    _rigidbody.velocity += transform.forward * 2;
        //}

    }

    Vector3 CalculateRelativeSpeed(Vector3 _objectPos, Vector3 _relativePos, Vector3 _lastPos)
    {
        var Speed = ((_objectPos - _relativePos) - _lastPos) / Time.deltaTime;
        _lastPos = _objectPos - _relativePos;
        return Speed;
    }
}
