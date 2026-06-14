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
using Atbt.Character;
using Atbt.Location;

namespace Atbt.Time {
[CreateAssetMenu(menuName = "ATBT/Time/Calendar Event")]
public class CalendarEventObject : ScriptableObject {
#region -------------------- Public Variables --------------------
    public string Id;
    public string DisplayName;
        
    [TextArea]
    public string Description;

    public int Date;
    public int StartHour;
    public int EndHour;

    public bool CanGiveGifts;
    public bool CanTalkToAll;
    public bool IsShopAvailable;

    public CalendarEventTypeEnum EventType;
    public LocationObject Location;
    public SeasonTypeEnum Season;
    
    public List<NpcObject> Attendees;
#endregion
}}