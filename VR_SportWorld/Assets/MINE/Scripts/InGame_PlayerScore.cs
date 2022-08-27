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

        text_timer.text = "Time: " + (int)fl_timer;
        text_kcal.text = "Energy: " + fl_kcal.ToString("0.0 kcal");
    }

    public void EndGame()
    {
        go_endScreen.SetActive(true);
        go_pauseScreen.SetActive(false);
        go_uiHelpers.SetActive(true);

        Time.timeScale = 0;

        text_endscore.text = "FINAL SCORE: " + int_score;
        text_endtime.text = "TOTAL TIME: " + (int)(fl_timer/60) + " minutes";
        
        text_endkcal.text = "ENERGY BURNT: " + fl_kcal.ToString("0.00 kcal");

        _sportPlayer.TodayBurntKcal += fl_kcal;
        _json.SavePlayerToJson(_sportPlayer);
    }

    public void PauseGame()
    {
        go_pauseScreen.SetActive(true);
        go_uiHelpers.SetActive(true);

        Time.timeScale = 0;
    }
}
