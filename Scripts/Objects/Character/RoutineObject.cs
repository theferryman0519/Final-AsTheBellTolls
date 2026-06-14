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

namespace Atbt.Character {
[CreateAssetMenu(menuName = "ATBT/Character/Routine")]
public class RoutineObject : ScriptableObject {
#region -------------------- Public Variables --------------------
    public string Id;
    public string DisplayName;
        
    [TextArea]
    public string Description;
    
    public string NpcId;
    
    public List<int> Timings;
    
    public List<WaypointObject> Waypoints;
#endregion
}}