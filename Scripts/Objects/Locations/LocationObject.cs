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
[CreateAssetMenu(menuName = "Objects/Location")]
public class LocationObject : ScriptableObject {
    public string Id;
    public string DisplayName;
    public string CorrespondingScene;
    // public string CorrespondingAudio;
}}
