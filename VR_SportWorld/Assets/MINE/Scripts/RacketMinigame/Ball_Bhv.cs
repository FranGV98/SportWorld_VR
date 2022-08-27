using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Bhv : MonoBehaviour
{
    public GameObject explosion;
    float lifetimer;
    InGame_PlayerScore _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("CommonVRPlayer").GetComponent<InGame_PlayerScore>();
    }

    // Update is called once per frame
    void Update()
    {
        lifetimer += Time.deltaTime;

        if(lifetimer > 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Finish")
        {
            GameObject.Instantiate(explosion, transform.position, transform.rotation);
            _player.int_score += 100;
            _player.text_score.text = "Score: " + _player.int_score;
            Destroy(gameObject);
        }

        if(col.tag == "Damagable")
        {
            _player.EndGame();
            Destroy(gameObject);
        }
    }
}
