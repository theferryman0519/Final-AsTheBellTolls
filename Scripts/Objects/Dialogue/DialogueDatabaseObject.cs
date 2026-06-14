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

namespace Atbt.Dialogue {
[CreateAssetMenu(menuName = "ATBT/Dialogue/Dialogue Database")]
public class DialogueDatabaseObject : ScriptableObject {
#region -------------------- Public Variables --------------------
    public string Id;
    public string DisplayName;
        
    [TextArea]
    public string Description;
    
    public List<DialogueObject> Dialogues;
#endregion
}}