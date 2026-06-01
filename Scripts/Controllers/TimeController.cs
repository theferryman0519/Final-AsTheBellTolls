// Main Dependencies
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [Header("Time Actions")]
    public event Action<int, int> OnTimeTick;
    public event Action OnHourTick;
    public event Action OnDayTick;
    public event Action OnSeasonTick;
    public event Action OnYearTick;
    public event Action OnDayEnd;
#endregion
#region -------------------- Private Variables --------------------
    private int _minutesPerTick = 10;
    private int _daysPerWeek = 7;
    private int _daysPerSeason = 31;
    private int _seasonsPerYear = 4;

    private float _realSecondsPerTick = 8.5f;
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

                AdvanceTime();
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

    public int TotalMinutesElapsed()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Getting the total minutes that has elapsed for the day");

        return (HourNumber * 60) + MinuteNumber;
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
        
        if (HourNumber >= currentSeason.DawnHours.StartHour && HourNumber < currentSeason.DawnHours.EndHour)
        {
            return DaylightTypeEnum.Dawn;
        }

        if (HourNumber >= currentSeason.DayHours.StartHour && HourNumber < currentSeason.DayHours.EndHour)
        {
            return DaylightTypeEnum.Day;
        }

        if (HourNumber >= currentSeason.DuskHours.StartHour && HourNumber < currentSeason.DuskHours.EndHour)
        {
            return DaylightTypeEnum.Dusk;
        }

        return DaylightTypeEnum.Night;
    }

    public void AdvanceNapTime()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Advancing the time from a nap");

        IsTimeRunning = false;

        int totalMinutes = TotalMinutesElapsed();

        totalMinutes += 180; // 3 hours

        if ((totalMinutes % 60) != 0)
        {
            totalMinutes += 60 - (totalMinutes % 60);
        }

        HourNumber = totalMinutes / 60;
        MinuteNumber = 0;

        OnTimeTick?.Invoke();
        OnHourTick?.Invoke();

        IsTimeRunning = true;
    }

    public void StartEndOfDay()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Starting the end of day");

        IsTimeRunning = false;

        OnDayEnd?.Invoke();

        // TODO
        // End of day logic
        // Auto save game
        
        AdvanceDay();

        HourNumber = 6;
        MinuteNumber = 0;

        OnDayTick?.Invoke();

        IsTimeRunning = true;
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

    private void AdvanceTime()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Advancing the minutes and hours");

        int previousHour = HourNumber;

        MinuteNumber += _minutesPerTick;

        while (MinuteNumber >= 60)
        {
            MinuteNumber -= 60;
            HourNumber++;
        }

        OnTimeTick?.Invoke(HourNumber, MinuteNumber);

        if (HourNumber != previousHour)
        {
            OnHourTick?.Invoke();
        }

        if (HourNumber >= 24)
        {
            StartEndOfDay();
        }
    }

    private void AdvanceDay()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Advancing the day");

        DayNumber += 1;

        WeekdayType = (WeekdayTypeEnum)(((int)WeekdayType + 1) % _daysPerWeek);

        if (DayNumber > _daysPerSeason)
        {
            DayNumber = 1;

            AdvanceSeason();
        }
    }

    private void AdvanceSeason()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Advancing the season and year");

        SeasonType = (SeasonTypeEnum)(((int)SeasonType + 1) % _seasonsPerYear);

        OnSeasonTick?.Invoke();

        if (SeasonType == SeasonTypeEnum.Spring)
        {
            YearNumber += 1;

            OnYearTick?.Invoke();
        }
    }
#endregion
}}
