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

namespace Atbt.Time {
public enum CalendarEventTypeEnum {
#region -------------------- Enum List --------------------
    None = 0,
    Anniversary,
    Birthday,
    MainFestival,
    MiniFestival,
    OngoingEvent,
#endregion
}}