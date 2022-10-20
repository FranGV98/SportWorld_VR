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
    public Text text_final;
    public GameObject go_pauseScreen, go_endScreen, go_uiHelpers;

    public JSON_Writter _json;
    private SportPlayer tempPlayer;

    public int gameUID;

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
        tempPlayer = _json.LoadPlayerFromJson();

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(tempPlayer == null)
        {
            tempPlayer = _json.LoadPlayerFromJson();
        }
        else
        {
            fl_timer += Time.deltaTime;
            fl_kcal = (fl_timer / 60) * 5 * 3.5f * (float)tempPlayer.weight / 200; //MET FORMULA: minutes * MET * 3.5 * Kg / 200

            text_timer.text = "" + DisplayTime(fl_timer);
            text_kcal.text = "" + fl_kcal.ToString("0.0");
        }    
    }

    public void EndGame()
    {
        go_endScreen.SetActive(true);
        go_pauseScreen.SetActive(false);
        go_uiHelpers.SetActive(true);

        Time.timeScale = 0;

        CheckScores();

        text_final.text = "FINAL SCORE: " + int_score + "   MAX SCORE: " + tempPlayer.MinigamesInfo[gameUID].MaxScore +
            "\nTOTAL TIME: " + DisplayTime(fl_timer) + "    MAX TIME: " + DisplayTime(tempPlayer.MinigamesInfo[gameUID].MaxTime) +
            "\nENERGY BURNT: " + fl_kcal.ToString("0.00 kcal");

        tempPlayer.TodayBurntKcal += (int)fl_kcal;
        tempPlayer.TodayActivityTime += (int)fl_timer;
        _json.SavePlayerToJson(tempPlayer);

    }

    public void SaveValues()
    {
        tempPlayer.TodayBurntKcal += (int)fl_kcal;
        tempPlayer.TodayActivityTime += (int)fl_timer;
        _json.SavePlayerToJson(tempPlayer);
    }

    public void PauseGame()
    {
        go_pauseScreen.SetActive(true);
        go_uiHelpers.SetActive(true);

        Time.timeScale = 0;
    }

    public void CheckKcalObjective()
    {
        if(tempPlayer.TodayBurntKcal >= tempPlayer.KcalObjective)
        {
            tempPlayer.ActivityRegister[tempPlayer.ActivityRegister.Count].ObjectiveReached = true;
        }
    }

    public void CheckScores()
    {
        if(int_score > tempPlayer.MinigamesInfo[gameUID].MaxScore)
        {
            tempPlayer.MinigamesInfo[gameUID].MaxScore = int_score;
        }

        if(fl_timer > tempPlayer.MinigamesInfo[gameUID].MaxTime)
        {
            tempPlayer.MinigamesInfo[gameUID].MaxTime = fl_timer;
        }
    }

    public void AddScore(int _score)
    {
        int_score += _score;
        text_score.text = "" + int_score;
    }

    string DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        string _displayTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        return _displayTime;
    }
}
