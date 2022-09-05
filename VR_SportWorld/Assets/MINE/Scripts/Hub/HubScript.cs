using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HubScript : MonoBehaviour
{
    public JSON_Writter json;
    public SportPlayer tempPlayer;

    public CalendarScript calendar;

    public GameObject usercreation_Screen;
    public GameObject calendar_Screen, main_Screen;

    //Main Screen
    public Text mainusername_text;
    public Text mainKcal_text;

    //SetCharacter Screen
    public InputField PlayerName_InputField;
    public Text weight_text, age_text, kcalobj_text;

    // Start is called before the first frame update
    void Start()
    {
        json = gameObject.GetComponent<JSON_Writter>();

        if (File.Exists(Application.dataPath + "/MINE/SavedData/PlayerDataFile.json"))
        {
            tempPlayer = json.LoadPlayerFromJson();
            CheckIfCreateNewDay(tempPlayer.ActivityRegister[tempPlayer.ActivityRegister.Count-1]);
        }
        else
        {
            CreateNewUser();
        }

        UpdateScreenInfo();

        //calendar.PlayerActivityDays = tempPlayer.ActivityRegister;
        //print(tempPlayer.ActivityRegister.Count);
        //CheckIfCreateNewDay(tempPlayer.ActivityRegister[tempPlayer.ActivityRegister.Count]);
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
        tempPlayer.name = PlayerName_InputField.text;
        json.SavePlayerToJson(tempPlayer);
        Debug.Log("Player Updated");
    }
    void CreateNewUser()
    {
        usercreation_Screen.SetActive(true);
        main_Screen.SetActive(false);

        json.SavePlayerToJson(new SportPlayer()); //create player default values

        tempPlayer = json.LoadPlayerFromJson(); //Equal to temporal player
    }
    void CheckIfCreateNewDay(ActivityDay _lastday) //Check if the last day played is equal to today to add a new register
    {
        if(_lastday.year != DateTime.Today.Year || _lastday.month != DateTime.Today.Month || _lastday.day != DateTime.Today.Day)
        {
            ActivityDay currentDay = new ActivityDay();
            tempPlayer.ActivityRegister.Add(currentDay);
            json.SavePlayerToJson(tempPlayer);
            Debug.Log("New day created");
        }
    }

    void UpdateScreenInfo()
    {
        if(main_Screen.activeSelf)
        {
            mainusername_text.text = tempPlayer.name;
            mainKcal_text.text = "Kcal burnt today: " + tempPlayer.TodayBurntKcal + " / " + tempPlayer.KcalObjective;
        }
    }
}
