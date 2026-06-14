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
using Atbt.Weather;

namespace Atbt.Dialogue {
[CreateAssetMenu(menuName = "ATBT/Dialogue/Dialogue")]
public class DialogueObject : ScriptableObject {
#region -------------------- Public Variables --------------------
    public string Id;
    public string DisplayName;
        
    [TextArea]
    public string Description;
    
    public string CharacterId;
    public string RequiredQuestId;

    public int Priority;
    public int RequiredHearts;
    
    public bool OnceOnly;
    
    public RemarkTypeEnum RemarkType;
    public WeatherTypeEnum RequiredWeather;

    public List<string> Texts;
    public List<string> Options;
    public List<int> OptionPoints;
#endregion
}}