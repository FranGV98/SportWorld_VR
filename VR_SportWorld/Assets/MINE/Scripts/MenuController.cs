using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public InGame_PlayerScore _player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //SCENE MANAGEMENT
    public void LoadHub()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Hub");
    }

    public void LoadLegGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LegMinigame");
    }

    public void LoadRacketGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TennisMinigame");       
    }

    public void ReloadGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadFlyingGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("FlyingMinigame");
    }

    //PAUSE MENU
    public void ResumeGame()
    {
        Time.timeScale = 1;
        if(_player != null)
        {
            _player.go_pauseScreen.SetActive(false);
            _player.go_uiHelpers.SetActive(false);
        }
    }
}
