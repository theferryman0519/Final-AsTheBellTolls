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
using Atbt.Enum;

namespace Atbt.Object {
[CreateAssetMenu(menuName = "Objects/Calendar Event")]
public class CalendarEventObject : ScriptableObject {
    public string Id;
    public string DisplayName;
    public string Description;

    public int Date;
    public int StartHour;
    public int EndHour;

    public bool CanTalkToAll;
    public bool CanGiveGifts;
    public bool AreItemsSold;

    public SeasonTypeEnum Season;
    public CalendarEventTypeEnum EventType;

    public LocationObject Location;
}}
