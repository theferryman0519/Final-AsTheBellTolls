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
public class MinigameFishingProgress : MinigameProgress {
    
#region -------------------- Variables --------------------
#region ---------- Public Variables ----------
    public MinigameFishingStateType ActivityState { get; set; }

    public float HookTimer { get; set; }
    public float CurrentTimer { get; set; }
    public float CurrentDialDegree { get; set; }

    public int FishMidDegree { get; set; }
    public int SuccessLowDegree { get; set; }
    public int SuccessHighDegree { get; set; }

    public bool IsUsingRod { get; set; }
#endregion
#endregion
}}
