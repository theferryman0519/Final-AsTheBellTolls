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
using Atbt.Item;

namespace Atbt.Time {
[CreateAssetMenu(menuName = "ATBT/Time/Season")]
public class SeasonObject : ScriptableObject {
#region -------------------- Public Variables --------------------
    public string Id;
    public string DisplayName;
        
    [TextArea]
    public string Description;

    public int ClearDaysCount;
    public int CloudyDaysCount;
    public int RainyDaysCount;
    public int RainySevereDaysCount;
    public int SnowyDaysCount;
    public int SnowySevereDaysCount;
    public int WindyDaysCount;
    
    public (int Start, int End) DawnHours;
    public (int Start, int End) DayHours;
    public (int Start, int End) DuskHours;
    public (int Start, int End) NightHours;
    
    public SeasonTypeEnum SeasonType;
    
    public List<ItemObject> InSeasonCrops;
    public List<ItemObject> InSeasonFish;
    public List<ItemObject> InSeasonFlowers;
    public List<ItemObject> InSeasonHerbs;
    public List<ItemObject> InSeasonWood;
    public List<int> SetClearDays;
#endregion
}}