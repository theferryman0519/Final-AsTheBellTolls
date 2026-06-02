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

namespace Atbt.Object {
[CreateAssetMenu(menuName = "Objects/Routine")]
public class RoutineObject : ScriptableObject {
    public string Id;
    public string NpcId;

    public List<int> Timings;
    public List<WaypointObject> Waypoints;
}}
