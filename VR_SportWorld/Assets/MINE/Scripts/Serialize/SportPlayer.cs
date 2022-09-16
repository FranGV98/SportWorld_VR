using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SportPlayer
{
    public string name = "user";
    public double weight = 80;
    public int age = 13;
    public int UID = 0;
    public List<MiniGameInfo> MinigamesInfo;
    public float TodayBurntKcal;
    public float TodayActivityTime;
    public int KcalObjective = 10;
    public List<ActivityDay> ActivityRegister;
    public int numMinigames;

    public SportPlayer()
    {
        name = "User";
        weight = 50;
        age = 20;
        UID = 0;
        TodayBurntKcal = 0;
        KcalObjective = 400;
        numMinigames = 0;
        MinigamesInfo = new List<MiniGameInfo>();
        ActivityRegister = new List<ActivityDay>();
        ActivityRegister.Add(new ActivityDay());
    }
}

[System.Serializable]
public class ActivityDay
{
    public int day = 0;
    public int month = 0;
    public int year = 0;

    public int EnergyBurnt = 0;
    public float ActivityTime;
    public bool ObjectiveReached = false;

    public ActivityDay()
    {
        day = DateTime.Today.Day;
        month = DateTime.Today.Month;
        year = DateTime.Today.Year;

        EnergyBurnt = 0;
        ActivityTime = 0;
        ObjectiveReached = false;
    }
}

[System.Serializable]
public class MiniGameInfo
{
    //Minigames: [0] Leg, [1] Racket, [2] Flying
    public int UID;
    public int MaxScore;
    public float MaxTime;

    public MiniGameInfo(int uid)
    {
        UID = uid;
        MaxScore = 0;
        MaxTime = 0;
    }
}

