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

namespace Atbt.Animal {
public enum AnimalActivityStateEnum {
#region -------------------- Enum List --------------------
    Idle = 0,
    Walking,
    Eating,
    Drinking,
    Sleeping
#endregion
}}