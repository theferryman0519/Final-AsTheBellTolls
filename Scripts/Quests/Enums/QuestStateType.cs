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

namespace AsTheBellTolls.Quests {
public enum QuestStateType {

#region -------------------- ENUM --------------------
    Inactive = 0,
    Available,
    Active,
    Completed,
    Failed,
    Expired,

#endregion
}}