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
using Atbt.Core;
using Atbt.Enum;

namespace Atbt.Controller {
public class WeatherController : Singleton<WeatherController> {

#region -------------------- Serialized Variables --------------------

#endregion
#region -------------------- Public Variables --------------------
    [Header("Current Weather")]
    public WeatherTypeEnum CurrentWeather;
#endregion
#region -------------------- Private Variables --------------------

#endregion
#region -------------------- Initial Functions --------------------

#endregion
#region -------------------- Coroutines --------------------

#endregion
#region -------------------- Public Methods --------------------
    // Controls daily weather
    // Controls weather rules
    // Controls seasonal weather restrictions

    public void InitializeController()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Initializing the weather controller");

        CoreController.Inst.LoadingStepCompleted();
    }

    public WeatherTypeEnum GetCurrentWeather()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Getting the current weather");

        return CurrentWeather;
    }
#endregion
#region -------------------- Private Methods --------------------

#endregion
}}
