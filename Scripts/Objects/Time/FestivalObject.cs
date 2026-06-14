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
using Atbt.Festival;

namespace Atbt.Time {
[CreateAssetMenu(menuName = "ATBT/Time/Festival")]
public class FestivalObject : CalendarEventObject {
#region -------------------- Public Variables --------------------
    public FestivalMovementEnum MovementType;
    public FestivalSettingEnum Setting;
#endregion
}}