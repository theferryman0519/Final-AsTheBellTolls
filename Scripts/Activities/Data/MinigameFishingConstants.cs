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

namespace AsTheBellTolls.Activities {
public class MinigameFishingConstants {
    
#region -------------------- Variables --------------------
#region ---------- Constant Variables ----------
    // Timers
    public const float MinHookSeconds = 0.5f;
    public const float MaxHookSeconds = 3.5f;
    public const float NetUseSeconds = 2.0f;
    public const float MinigameTimer = 7.0f;

    // Success Area Degrees
    public const int SuccessDegreesBase = 5;
    public const int SuccessDegreesCopper = 8;
    public const int SuccessDegreesIron = 12;
    public const int SuccessDegreesSilver = 15;
    public const int SuccessDegreesGold = 20;
    public const int SuccessDegreesCobalt = 30;

    // Fish Position
    public const int MinFishDegree = 30;
    public const int MaxFishDegree = 330;

    // Speeds
    public const float DialTurnSpeed = 5.0f;
#endregion
#endregion
}}
