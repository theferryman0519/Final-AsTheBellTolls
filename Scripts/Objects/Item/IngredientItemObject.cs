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
using Atbt.Location;
using Atbt.Time;
using Atbt.Weather;

namespace Atbt.Item {
[CreateAssetMenu(menuName = "ATBT/Item/Ingredient Item")]
public class IngredientItemObject : ItemObject {
#region -------------------- Public Variables --------------------
    public List<DaylightTypeEnum> FoundDaylight;
    public List<LocationObject> FoundLocations;
    public List<SeasonTypeEnum> FoundSeasons;
    public List<WeatherTypeEnum> FoundWeather;
#endregion
}}