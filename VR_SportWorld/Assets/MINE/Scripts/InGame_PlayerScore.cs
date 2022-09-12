using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame_PlayerScore : MonoBehaviour
{
    [HideInInspector]
    public int int_score;
    public float fl_timer, fl_kcal;

    public Text text_score, text_kcal, text_timer;
    public Text text_endscore, text_endtime, text_endkcal;
    public GameObject go_pauseScreen, go_endScreen, go_uiHelpers;

    private JSON_Writter _json;
    private SportPlayer _sportPlayer;

    public enum Minigame
    {
        LegGame,
        RacketGame,
        flyingGame,
        PausedGame
    }

    public Minigame currentMinigame;

    void Start()
    {
        _json = GameObject.Find("MenusManager").GetComponent<JSON_Writter>();
        _sportPlayer = _json.LoadPlayerFromJson();

        //print(_sportPlayer.TodayBurntKcal);
    }

    // Update is called once per frame
    void Update()
    {
        fl_timer += Time.deltaTime;
        fl_kcal = (fl_timer / 60) * 5 * 3.5f * (float)_sportPlayer.weight / 200; //MET FORMULA: minutes * MET * 3.5 * Kg / 200

        text_timer.text = "Time: " + DisplayTime(fl_timer);
        text_kcal.text = "Energy: " + fl_kcal.ToString("0.0 kcal");
    }

    public void EndGame()
    {
        go_endScreen.SetActive(true);
        go_pauseScreen.SetActive(false);
        go_uiHelpers.SetActive(true);



        Time.timeScale = 0;

        text_endscore.text = "FINAL SCORE: " + int_score;
        text_endtime.text = "TOTAL TIME: " + DisplayTime(fl_timer);
        
        text_endkcal.text = "ENERGY BURNT: " + fl_kcal.ToString("0.00 kcal");

        _sportPlayer.TodayBurntKcal += (int)fl_kcal;
        _sportPlayer.TodayActivityTime += (int)fl_timer;
        _json.SavePlayerToJson(_sportPlayer);
    }

    public void PauseGame()
    {
        go_pauseScreen.SetActive(true);
        go_uiHelpers.SetActive(true);

        Time.timeScale = 0;
    }

    public void CheckKcalObjective()
    {
        if(_sportPlayer.TodayBurntKcal >= _sportPlayer.KcalObjective)
        {
            _sportPlayer.ActivityRegister[_sportPlayer.ActivityRegister.Count].ObjectiveReached = true;
        }
    }

    public void AddScore(int _score)
    {
        int_score += _score;
        text_score.text = "Score: " + int_score;
    }

    string DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        string _displayTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        return _displayTime;
    }
}
