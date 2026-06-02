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
[CreateAssetMenu(menuName = "Objects/Waypoint")]
public class WaypointObject : ScriptableObject {
    public string Id;
    public string CorrespondingScene;

    public int PosX;
    public int PosY;
}}
