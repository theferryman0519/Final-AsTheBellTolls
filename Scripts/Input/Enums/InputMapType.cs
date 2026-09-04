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

namespace AsTheBellTolls.Input {
public enum InputMapType {

#region -------------------- ENUM --------------------
    None = 0,
    Gameplay,
    Ui,
    Dialogue,
    Fishing,
    TonicMaking,
    Cinematic,
    AutomaticEvent,

#endregion
}}