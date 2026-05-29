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
using Atbt.Object;

namespace Atbt.Controller {
public class TimeController : Singleton<TimeController> {

#region -------------------- Serialized Variables --------------------
    [Header("Season Objects")]
    [SerializeField] private List<SeasonObject> SeasonsList = new();
#endregion
#region -------------------- Public Variables --------------------
    [Header("Time Elements")]
    public int YearNumber;
    public int DayNumber;
    public int HourNumber;
    public int MinuteNumber;

    public SeasonTypeEnum SeasonType;
    public WeekdayTypeEnum WeekdayType;

    public bool IsTimeRunning;
#endregion
#region -------------------- Private Variables --------------------
    private int _minutesPerTick = 10;

    private float _realSecondsPerTick = 7f;
    private float _timeAccumulator = 0f;
#endregion
#region -------------------- Initial Functions --------------------
    void Update()
    {
        if (IsTimeRunning)
        {
            _timeAccumulator += Time.deltaTime;

            if (_timeAccumulator >= _realSecondsPerTick)
            {
                _timeAccumulator -= _realSecondsPerTick;

                AdvanceTime(_minutesPerTick);
            }
        }
    }
#endregion
#region -------------------- Coroutines --------------------

#endregion
#region -------------------- Public Methods --------------------
    // Controls the day time
    // Controls the game clock
    // Controls the season
    // Controls the year
    // Controls the player bedtime
    // Controls the day type transitions

    public void InitializeController()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Initializing the time controller");

        CoreController.Inst.LoadingStepCompleted();
    }

    public string GetCurrentDate()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Getting the current calendar date");

        return $"{WeekdayType}, {GetDayNumberString()} of {SeasonType}, Year {YearNumber}";
    }

    public string GetCurrentTime()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Getting the current time");

        string period = HourNumber >= 12 ? "PM" : "AM";
        int displayHour = HourNumber % 12;

        if (displayHour == 0)
        {
            displayHour = 12;
        }

        return $"{displayHour}:{MinuteNumber:00} {period}";
    }

    public DaylightTypeEnum GetCurrentDaylight()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Getting the current daylight type");

        SeasonObject currentSeason = SeasonsList.FirstOrDefault(s => s.SeasonType == SeasonType);

        if (currentSeason == null)
        {
            return DaylightTypeEnum.Day;
        }

        if (HourNumber is >= currentSeason.DawnHours.startHour and < currentSeason.DawnHours.endHour)
        {
            return DaylightTypeEnum.Dawn;
        }

        else if (HourNumber is >= currentSeason.DayHours.startHour and < currentSeason.DayHours.endHour)
        {
            return DaylightTypeEnum.Day;
        }

        else if (HourNumber is >= currentSeason.DuskHours.startHour and < currentSeason.DuskHours.endHour)
        {
            return DaylightTypeEnum.Dusk;
        }

        else
        {
            return DaylightTypeEnum.Night;
        }
    }
#endregion
#region -------------------- Private Methods --------------------
    private string GetDayNumberString()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Getting the current day number string");

        if (DayNumber % 100 >= 11 && DayNumber % 100 <= 13)
        {
            return $"{DayNumber}th";
        }

        switch (DayNumber % 10)
        {
            case 1: return $"{DayNumber}st";
            case 2: return $"{DayNumber}nd";
            case 3: return $"{DayNumber}rd";
            default: return $"{DayNumber}th";
        }
    }

    private void AdvanceTime(int minutes)
    {
        MinuteNumber += minutes;

        while (MinuteNumber >= 60)
        {
            MinuteNumber -= 60;
            HourNumber++;
        }

        if (HourNumber >= 24)
        {
            HourNumber = 0;
        }
    }
#endregion
}}
