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
    [Header("Canvas Elements")]
    [SerializeField] private CanvasGroup CanvasElement;
    [SerializeField] private CanvasGroup QuestCanvas;
    [SerializeField] private CanvasGroup InteractCanvas;
    [SerializeField] private CanvasGroup TimeManipulationCanvas;
    [SerializeField] private CanvasGroup NotificationCanvas;

    [Header("Text Objects")]
    [SerializeField] private AtbtText DateText;
    [SerializeField] private AtbtText TimeText;
    [SerializeField] private AtbtText LocationText;
    [SerializeField] private AtbtText InteractText;

    [Header("Icon Elements")]
    [SerializeField] private Image DaylightIcon;
    [SerializeField] private Image WeatherIcon;

    [Header("Icon Sprites")]
    [SerializeField] private List<UnityEngine.Sprite> DaylightSprites = new();
    [SerializeField] private List<UnityEngine.Sprite> WeatherSprites = new();

    [Header("Slider Elements")]
    [SerializeField] private AtbtSlider StaminaSlider;
    [SerializeField] private AtbtSlider TimeManipulationSlider;
#endregion
#region -------------------- Public Variables --------------------
    [Header("Canvas Group")]
    public CanvasGroup MainCanvas => CanvasElement;

    public event Action UpdateStamina;
    public event Action UpdateTimeManipulation;
#endregion
#region -------------------- Private Variables --------------------
    
#endregion
#region -------------------- Initial Functions --------------------
    void Start()
    {
        UpdateDate();
        UpdateTime(TimeController.Inst.HourNumber, TimeController.Inst.MinuteNumber);
        UpdateLocation();
        UpdateDaylightIcon();
        UpdateWeatherIcon();
        UpdateStaminaSlider();
        UpdateTimeManipulationSlider();
    }

    void OnEnable()
    {
        UpdateStamina += UpdateStaminaSlider;
        UpdateTimeManipulation += UpdateTimeManipulationSlider;

        TimeController.Inst.OnTimeTick += UpdateTime;
        TimeController.Inst.OnHourTick += UpdateDaylightIcon;
        TimeController.Inst.OnDayTick += UpdateDate;
    }

    void OnDisable()
    {
        UpdateStamina -= UpdateStaminaSlider;
        UpdateTimeManipulation -= UpdateTimeManipulationSlider;

        TimeController.Inst.OnTimeTick -= UpdateTime;
        TimeController.Inst.OnHourTick -= UpdateDaylightIcon;
        TimeController.Inst.OnDayTick -= UpdateDate;
    }
#endregion
#region -------------------- Coroutines --------------------
    
#endregion
#region -------------------- Public Methods --------------------
    
#endregion
#region -------------------- Private Methods --------------------
    private void UpdateDate()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the date text");

        DateText.SetText(TimeController.Inst.GetCurrentDate());
    }

    private void UpdateTime(int hours, int minutes)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the time text");

        TimeText.SetText(TimeController.Inst.GetCurrentTime());
    }

    private void UpdateLocation()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the location text");

        LocationObject location = LocationController.Inst.GetCurrentLocation();
        string locationName = "Unknown Location";

        if (location != null)
        {
            locationName = location.DisplayName;
        }

        LocationText.SetText(locationName);
    }

    private void UpdateDaylightIcon()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the daylight icon");

        UnityEngine.Sprite daylightSprite = DaylightSprites[0];
        DaylightTypeEnum currentDaylight = TimeController.Inst.GetCurrentDaylight();
        int daylightInt = (int)currentDaylight;

        if (daylightInt < DaylightSprites.Count && daylightInt >= 0)
        {
            daylightSprite = DaylightSprites[daylightInt];
        }

        DaylightIcon.sprite = daylightSprite;
    }

    private void UpdateWeatherIcon()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the weather icon");

        UnityEngine.Sprite weatherSprite = WeatherSprites[0];
        WeatherTypeEnum currentWeather = WeatherController.Inst.GetCurrentWeather();
        int weatherInt = (int)currentWeather;

        if (weatherInt < WeatherSprites.Count && weatherInt >= 0)
        {
            weatherSprite = WeatherSprites[weatherInt];
        }

        WeatherIcon.sprite = weatherSprite;
    }
    
    private void UpdateStaminaSlider()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the stamina slider");

        int stamina = PlayerController.Inst.GetPlayerStamina();

        StaminaSlider.SetAmount(stamina);
    }

    private void UpdateTimeManipulationSlider()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the time manipulation slider");

        // int timeManipulation = TimeManipulationController.Inst.GetTimeManipulationEnergy();
        //
        // TimeManipulationSlider.SetAmount(timeManipulation);
    }
#endregion
}}
