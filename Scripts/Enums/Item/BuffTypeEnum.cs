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

namespace Atbt.Item {
public enum BuffTypeEnum {
#region -------------------- Enum List --------------------
    None = 0,
    GatheringAmount,
    GatheringDouble,
    GatheringQuality,
    SpeedIncrease,
    SocialIncrease,
    StaminaMax,
    StaminaSlow,
#endregion
}}