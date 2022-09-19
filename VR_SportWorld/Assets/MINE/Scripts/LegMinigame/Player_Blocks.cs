using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;

public class Player_Blocks : MonoBehaviour
{
    public InGame_PlayerScore _playerscoreSys;
    public GameObject HUD;
    private BlockManager _blockManager;

    public AudioSource ScoreSFX, DeathSFX;

    void Start()
    {
        _blockManager = GameObject.Find("Blocks_Manager").GetComponent<BlockManager>();
        //_playerscoreSys = gameObject.GetComponent<InGame_PlayerScore>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Damagable"))
        {
            print("Muerto");
            //_playerscoreSys.text_kcal.text = "State: Dead";
        }

        if (col.gameObject.layer == LayerMask.NameToLayer("Scorer"))
        {
            _playerscoreSys.int_score += 100;
            print("Score = " + _playerscoreSys.int_score);
            _playerscoreSys.text_score.text = "Score: " + _playerscoreSys.int_score;

            if(col.tag == "Up")
            {
                _blockManager.int_UpScorerCount++;
                _blockManager.int_DownScorerCount = 0;
            }

            if (col.tag == "Down")
            {
                _blockManager.int_DownScorerCount++;
                _blockManager.int_UpScorerCount = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Damagable"))
        {
            Destroy(other.gameObject);
            DeathSFX.Play();
            _playerscoreSys.EndGame();
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Scorer"))
        {
            _blockManager.ColorTransition();
            ScoreSFX.Play();
            Destroy(other.transform.parent.gameObject);
        }

        if(other.gameObject.name == "Measurer")
        {
            _blockManager.fl_playerHeigth = transform.position.y;
            HUD.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
