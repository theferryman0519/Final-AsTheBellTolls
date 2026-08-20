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
public class MinigameTonicMakingProgress : MinigameProgress {
    
#region -------------------- Variables --------------------
#region ---------- Public Variables ----------
    public MinigameTonicMakingStateType ActivityState { get; set; }
    public MinigameTonicMakingColorType CurrentColor { get; set; }

    public float CurrentValveTime { get; set; }
    public float CurrentColorToColorTime { get; set; }

    public int HerbAmount { get; set; }

    public bool IsValveOpen { get; set; }
#endregion
#endregion
}}
