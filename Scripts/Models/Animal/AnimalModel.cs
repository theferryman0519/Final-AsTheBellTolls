// Main Dependencies
using System;
using System.Collections;
using System.Collections.Generic;
using Atbt.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// Game Dependencies

namespace Atbt.Animal {
public class AnimalModel {
#region -------------------- Public Variables --------------------
    public string CustomName;
    
    public int ByproductCount;
    public int ProductionDay;

    public bool HadDailyByproduct;
    public bool HadDailyFood;
    public bool HadDailyPet;
    
    public AnimalObject Animal;
    public QualityTypeEnum ByproductQuality;
    public AnimalActivityStateEnum CurrentActivity;
    public MaturityStateEnum MaturityState;
#endregion
#region -------------------- Public Methods --------------------

#endregion
}}