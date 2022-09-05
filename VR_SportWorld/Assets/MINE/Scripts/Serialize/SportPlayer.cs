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
    public List<int> MaxScore;
    public int TodayBurntKcal;
    public int KcalObjective = 10;
    public List<ActivityDay> ActivityRegister;

    public SportPlayer()
    {
        name = "user";
        weight = 50;
        age = 13;
        UID = 0;
        TodayBurntKcal = 0;
        KcalObjective = 400;
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
