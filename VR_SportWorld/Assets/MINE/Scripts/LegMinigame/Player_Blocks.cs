using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;

public class Player_Blocks : MonoBehaviour
{
    private int int_score;
    private float fl_timer, fl_kcal;
    public Text text_score, text_state, text_timer;
    public Text text_endscore, text_endtime, text_endkcal;
    public GameObject go_pauseScreen, go_endScreen;
    private BlockManager _blockManager;

    private JSON_Writter _json;
    private SportPlayer _sportPlayer;

    void Start()
    {
        _blockManager = GameObject.Find("Blocks_Manager").GetComponent<BlockManager>();
        _json = GameObject.Find("MenusManager").GetComponent<JSON_Writter>();
        _sportPlayer = _json.LoadPlayerFromJson();
        print(_sportPlayer.TodayBurntKcal);
    }

    void Update()
    {
        fl_timer += Time.deltaTime;
        text_timer.text = "Time: " + fl_timer;
        if(OVRInput.Get(OVRInput.Button.Start))
        {
            go_pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Damagable"))
        {
            print("Muerto");
            text_state.text = "State: Dead";
        }

        if (col.gameObject.layer == LayerMask.NameToLayer("Scorer"))
        {
            int_score += 100;
            print("Score = " + int_score);
            text_score.text = "Score: " + int_score;
            text_state.text = "State: Alive";

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
            EndGame();
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Scorer"))
        {
            _blockManager.ColorTransition();

            Destroy(other.transform.parent.gameObject);

        }
    }

    void EndGame()
    {
        go_endScreen.SetActive(true);
        Time.timeScale = 0;

        text_endscore.text = "FINAL SCORE: " + int_score;
        text_endtime.text = "TOTAL TIME: " + fl_timer;

        
        fl_kcal = (fl_timer / 60) * 5 * 3.5f * (float)_sportPlayer.weight / 200; //MET FORMULA: minutes * MET * 3.5 * Kg / 200
        text_endkcal.text = "KCAL BURNT: " + (double)fl_kcal;

        _sportPlayer.TodayBurntKcal += fl_kcal;
        _json.SavePlayerToJson(_sportPlayer);
    }
}
