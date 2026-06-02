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
[CreateAssetMenu(menuName = "Objects/Dialogue")]
public class DialogueObject : ScriptableObject {
    public string Id;
    public string NpcId;

    public List<string> Texts;
    public List<string> Options;
    public List<int> OptionPoints;
}}
