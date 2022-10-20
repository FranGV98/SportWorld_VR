using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingPlayer : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public GameObject go_Camera;

    public GameObject go_R_Hand, go_L_Hand;
    public float WingForce;
    Vector3 v3_R_LastPos, v3_L_LastPos;

    public Transform RealLHandPos, RealRHandPos;

    private FlyingManager _gameManager;
    private InGame_PlayerScore _playerScore;

    public Transform pos_rotReference;
    public Transform pos_frontReference;
    public Text testText;

    public AudioSource SFX;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = transform.GetComponent<Rigidbody>();

        v3_R_LastPos = go_R_Hand.transform.position - transform.position;
        v3_L_LastPos = go_L_Hand.transform.position - transform.position;

        _playerScore = transform.GetComponent<InGame_PlayerScore>();
        _gameManager = GameObject.Find("HalosManager").gameObject.GetComponent<FlyingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        var handsSpeed = MeasureHandsSpeed();

        VerticalFlyingSystem(handsSpeed.y);

        if(handsSpeed.magnitude > 1)
        {
            HorizontalFlyingSystem(handsSpeed.magnitude);
        }
    }

    Vector3 MeasureHandsSpeed()
    {
        //Calculate relative Speed of both hands respect the player position
        var RHand_Speed = ((go_R_Hand.transform.position - transform.position) - v3_R_LastPos) / Time.deltaTime;
        v3_R_LastPos = go_R_Hand.transform.position - transform.position;

        var LHand_Speed = ((go_L_Hand.transform.position - transform.position) - v3_L_LastPos) / Time.deltaTime;
        v3_L_LastPos = go_L_Hand.transform.position - transform.position;

        var HandsSpeed = RHand_Speed + LHand_Speed;

        return HandsSpeed;
    }

    void VerticalFlyingSystem(float verticalForce) //Uses the speed from hands to elevate the player
    {
        if (verticalForce < -1f && verticalForce > -16f & transform.position.y <= 15f)
        {
            Vector3 YForce = new Vector3(0, verticalForce * -WingForce);
            Vector3 FlightForce = pos_rotReference.transform.forward * Mathf.Abs(verticalForce * 3) + YForce;
            _rigidbody.AddForce(FlightForce);
        }

        if (_rigidbody.velocity.y <= -3.5f)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, -3.5f, _rigidbody.velocity.z);
        }
        if (_rigidbody.velocity.y >= 5f)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 5, _rigidbody.velocity.z);
        }    
    }

    void HorizontalFlyingSystem(float speedMagnitude)
    {
        testText.text = "" + speedMagnitude;

        var NoYRealHandpos = transform.TransformPoint(go_L_Hand.transform.position);
        NoYRealHandpos = new Vector3(NoYRealHandpos.x, 0, NoYRealHandpos.z);

        var NoYFrontRefPos = transform.TransformPoint(pos_frontReference.position);
        NoYFrontRefPos = new Vector3(NoYFrontRefPos.x, 0, NoYFrontRefPos.z);

        var FrontDist = Vector3.Distance(NoYRealHandpos, NoYFrontRefPos);

        if (FrontDist < 0.5f)
        {            
            Vector3 FlightHorForce = (pos_rotReference.transform.forward * Mathf.Abs(speedMagnitude) * 5) * -1;
            _rigidbody.AddForce(FlightHorForce);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Damagable")
        {
            Destroy(col.gameObject);
            _gameManager.num_leftHalos--;
            _playerScore.AddScore(100);
            _gameManager.time_countdown += 5;
            SFX.Play();
        }
    }
}
