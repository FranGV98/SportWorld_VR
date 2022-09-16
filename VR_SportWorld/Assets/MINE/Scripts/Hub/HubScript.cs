using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HubScript : MonoBehaviour
{
    JSON_Writter json;
    public SportPlayer tempPlayer;

    public CalendarScript calendar;
    
    //Screens: [0] User creation, [1] Main Screen, [2] Explanation Screen
    public List<GameObject> screens_List;

    //Panels: [0] Leg, [1] Racket, [2] Flying
    public List<GameObject> infoPanels_List;

    //Main Screen
    public Text mainusername_text;
    public Text mainKcal_text, activitytime_text;

    //SetCharacter Screen
    public InputField PlayerName_InputField;
    public Text weight_text, age_text, kcalobj_text;

    //Scoring System
    public Text ScoreGameInfo_text;
    public int count_Minigames; 

    // Start is called before the first frame update
    void Start()
    {
        json = gameObject.GetComponent<JSON_Writter>();

        if (File.Exists(Application.persistentDataPath + "/MINE/SavedData/playerdata.json"))
        {
            tempPlayer = json.LoadPlayerFromJson();
            CheckIfCreateNewDay(tempPlayer.ActivityRegister[tempPlayer.ActivityRegister.Count-1]);
        }
        else
        {
            CreateNewUser();
        }

        calendar.PlayerActivityDays = tempPlayer.ActivityRegister;

        CreateMinigamePlayerInfo();

        UpdateScreenInfo();
    }

    string DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        string _displayTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        return _displayTime;
    }
    public void AddWeight()
    {
        tempPlayer.weight += 1;
        weight_text.text = tempPlayer.weight + " Kg";
    }
    public void LoseWeight()
    {
        tempPlayer.weight -= 1;
        weight_text.text = tempPlayer.weight + " Kg";
    }

    public void AddAge()
    {
        tempPlayer.age += 1;
        age_text.text = tempPlayer.age + "";
    } 
    
    public void LoseAge()
    {
        tempPlayer.age -= 1;
        age_text.text = tempPlayer.age + "";
    }

    public void AddKcalObj()
    {
        tempPlayer.KcalObjective += 100;
        kcalobj_text.text = tempPlayer.KcalObjective + "";
    } 
    
    public void LoseKcalObj()
    {
        tempPlayer.KcalObjective -= 100;
        kcalobj_text.text = tempPlayer.KcalObjective + "";
    }

    public void SetPlayer()
    {
        //tempPlayer.name = PlayerName_InputField.text;
        json.SavePlayerToJson(tempPlayer);
        Debug.Log("Player Updated");
    }
    public void UpdateScreenInfo()
    {
        if(screens_List[1].activeSelf)
        {
            mainusername_text.text = tempPlayer.name;
            mainKcal_text.text = "Kcal burnt today: " + tempPlayer.TodayBurntKcal + " / " + tempPlayer.KcalObjective;
            activitytime_text.text = "Activity time: " + tempPlayer.TodayActivityTime/60 + " min";
        }
        if(screens_List[0].activeSelf)
        {
            weight_text.text = tempPlayer.weight + " Kg";
            age_text.text = tempPlayer.age + "";
            kcalobj_text.text = tempPlayer.KcalObjective + "";
        }
    }

    public void OpenScreen(int _gameUID)
    {
        for(int i = 0; i < screens_List.Count; i++)
        {
            screens_List[i].SetActive(false);
        }

        screens_List[_gameUID].SetActive(true);
    }

    public void OpenPanel(int _gameUID)
    {
        for (int i = 0; i < infoPanels_List.Count; i++)
        {
            infoPanels_List[i].SetActive(false);
        }
        infoPanels_List[_gameUID].SetActive(true);
        WriteScoreInfo(_gameUID);
    }

    void WriteScoreInfo(int _gameUID)
    {
        ScoreGameInfo_text.gameObject.SetActive(true);
        ScoreGameInfo_text.text = "Max Score: " + tempPlayer.MinigamesInfo[_gameUID].MaxScore +
            "\n Max Time: " + DisplayTime(tempPlayer.MinigamesInfo[_gameUID].MaxTime);
    }

    public void CloseAllPanels()
    {
        for (int i = 0; i < infoPanels_List.Count; i++)
        {
            infoPanels_List[i].SetActive(false);
        }
        ScoreGameInfo_text.gameObject.SetActive(false);
    }

    void CreateNewUser()
    {
        for (int i = 0; i < screens_List.Count; i++)
        {
            screens_List[i].SetActive(false);
        }

        screens_List[0].SetActive(true);

        json.SavePlayerToJson(new SportPlayer()); //create player default values

        tempPlayer = json.LoadPlayerFromJson(); //Equal to temporal player
    }
    void CheckIfCreateNewDay(ActivityDay _lastday) //Check if the last day played is equal to today to add a new register
    {
        if (_lastday.year != DateTime.Today.Year || _lastday.month != DateTime.Today.Month || _lastday.day != DateTime.Today.Day)
        {
            //Save last day exercise
            _lastday.EnergyBurnt = (int)tempPlayer.TodayBurntKcal;
            _lastday.ActivityTime = tempPlayer.TodayActivityTime; 
            if (tempPlayer.TodayBurntKcal >= tempPlayer.KcalObjective)
                _lastday.ObjectiveReached = true; 

            //Create new day
            ActivityDay currentDay = new ActivityDay();
            tempPlayer.ActivityRegister.Add(currentDay);

            //Reset current activity
            tempPlayer.TodayActivityTime = 0;
            tempPlayer.TodayBurntKcal = 0;

            json.SavePlayerToJson(tempPlayer);
            Debug.Log("New day created");
        }
    }

    //This function adds new minigame score info inside the player. Is thinked to be used in a future, every time a minigame is created.
    void CreateMinigamePlayerInfo()
    {
        if (tempPlayer.numMinigames < count_Minigames)
        {
            tempPlayer.MinigamesInfo.Add(new MiniGameInfo(tempPlayer.numMinigames));
            tempPlayer.numMinigames++;

            CreateMinigamePlayerInfo();
        }
        else
        {
            return;
        }
    }
    
}
