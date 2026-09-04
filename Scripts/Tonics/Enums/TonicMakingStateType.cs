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

namespace AsTheBellTolls.Tonics {
public enum TonicMakingStateType {

#region -------------------- ENUM --------------------
    Idle = 0,
    Adding,
    Mixing,
    Boiling,
    Completed,
    Failed,
    Cancelled,

#endregion
}}