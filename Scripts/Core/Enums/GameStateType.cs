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

namespace AsTheBellTolls.Core {
public enum GameStateType {
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
}}