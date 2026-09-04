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

namespace AsTheBellTolls.GameFlow {
public enum GameStateType {

#region -------------------- ENUM --------------------
    None = 0,
    Gameplay,
    Dialogue,
    Menu,
    Minigame,
    Cinematic,
    Festival,
    Transition,
    DayEnd,
    Paused,

#endregion
}}