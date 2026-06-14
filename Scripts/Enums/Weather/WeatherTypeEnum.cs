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

namespace Atbt.Weather {
public enum WeatherTypeEnum {
#region -------------------- Enum List --------------------
    Clear = 0,
    Cloudy,
    Rainy,
    RainySevere,
    Snowy,
    SnowySevere,
    Windy,
#endregion
}}