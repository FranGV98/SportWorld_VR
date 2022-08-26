using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject _player;
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
        SceneManager.LoadScene("Hub");
    }

    public void LoadLegGame()
    {
        SceneManager.LoadScene("LegMinigame");
    }

    public void LoadRacketGame()
    {

    }

    public void LoadBoxingGame()
    {

    }

    //PAUSE MENU
    public void ResumeGame()
    {
        Time.timeScale = 1;
        if(_player != null)
        {
            if (_player.GetComponent<InGame_PlayerScore>() != null)
                _player.GetComponent<InGame_PlayerScore>().go_pauseScreen.SetActive(false);
                _player.GetComponent<InGame_PlayerScore>().go_uiHelpers.SetActive(false);
        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
