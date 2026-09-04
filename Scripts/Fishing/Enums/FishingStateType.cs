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

namespace AsTheBellTolls.Fishing {
public enum FishingStateType {

#region -------------------- ENUM --------------------
    Idle = 0,
    Casting,
    Waiting,
    Hooked,
    Reeling,
    Completed,
    Failed,
    Cancelled,

#endregion
}}