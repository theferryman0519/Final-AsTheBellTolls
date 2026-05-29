// Main Dependencies
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// Game Dependencies
using Atbt.Enum;

namespace Atbt.Object {
[CreateAssetMenu(menuName = "Objects/Season")]
public class SeasonObject : ScriptableObject {
    public string Id;

    public SeasonTypeEnum SeasonType;

    public (int startHour, int endHour) DawnHours;
    public (int startHour, int endHour) DayHours;
    public (int startHour, int endHour) DuskHours;
    public (int startHour, int endHour) NightHours;

    public int ClearDayCount;
    public int CloudDayCount;
    public int RainDayCount;
    public int RainSevereDayCount;
    public int SnowDayCount;
    public int SnowSevereDayCount;
    public int WindDayCount;

    public List<int> SetClearDays;

    // public List<CropObject> InSeasonCrops;
    // public List<FishObject> InSeasonFish;
    // public List<FlowerObject> InSeasonFlowers;
    // public List<HerbObject> InSeasonHerbs;
    // public List<WoodObject> InSeasonWoods;
}}
