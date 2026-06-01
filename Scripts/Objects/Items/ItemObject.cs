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
[CreateAssetMenu(menuName = "Objects/Item")]
public class ItemObject : ScriptableObject {
    public string Id;
    public string DisplayName;
    public string Description;

    public ItemTypeEnum ItemType;

    public int PurchasePrice;
    public int ValuePrice;
    public int ReplenishmentAmount;
    public int GrowthDurationDays;
    public int GrowthManipulationDays;
    public int SpoilDurationDays;

    public bool UsedInReplenishment;
    public bool UsedInCooking;
    public bool UsedInCrafting;
    public bool UsedInInventions;
    public bool UsedAsGift;
    public bool CanHaveQualities;

    public List<SeasonTypeEnum> SeasonsAvailable;
    public List<DaylightTypeEnum> DaylightAvailable;
    public List<WeatherTypeEnum> WeatherAvailable;
}}
