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
using Atbt.Controller;
using Atbt.Enum;
using Atbt.Object;

namespace Atbt.Ui {
public class UiElementHud : MonoBehaviour {

#region -------------------- Serialized Variables --------------------
    [Header("Canvas Element")]
    [SerializeField] private Canvas CanvasElement;

    [Header("Text Objects")]
    [SerializeField] private AtbtText DateText;
    [SerializeField] private AtbtText TimeText;
    [SerializeField] private AtbtText LocationText;
    [SerializeField] private AtbtText InteractText;

    [Header("Icon Elements")]
    [SerializeField] private Image DaylightIcon;
    [SerializeField] private Image WeatherIcon;
#endregion
#region -------------------- Public Variables --------------------
    public Canvas MainCanvas => CanvasElement;
#endregion
#region -------------------- Private Variables --------------------
    
#endregion
#region -------------------- Initial Functions --------------------
    
#endregion
#region -------------------- Coroutines --------------------
    
#endregion
#region -------------------- Public Methods --------------------
    public void UpdateDate()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the date text");

        DateText.SetText(TimeController.Inst.GetCurrentDate());
    }

    public void UpdateTime()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the time text");

        TimeText.SetText(TimeController.Inst.GetCurrentTime());
    }

    public void UpdateLocation()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the location text");

        LocationObject location = LocationController.Inst.GetCurrentLocation();
        string locationName = "Unknown Location";

        if (location != null)
        {
            locationName = locationName.DisplayName;
        }

        LocationText.SetText(locationName);
    }

    public void UpdateDaylightIcon()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the daylight icon");

        DaylightTypeEnum currentDaylight = TimeController.Inst.GetCurrentDaylight();

        // TODO
        // Set DaylightIcon sprite from SpriteController based on current time
    }

    public void UpdateWeatherIcon()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the weather icon");

        // TODO
        // Set WeatherIcon sprite from SpriteController based on weather
    }
#endregion
#region -------------------- Private Methods --------------------
    
#endregion
}}
