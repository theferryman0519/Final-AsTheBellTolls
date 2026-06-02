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
using Atbt.Enum;

namespace Atbt.Item {
public class InteractModel : MonoBehaviour {
#region -------------------- Public Variables --------------------
    public QualityTypeEnum LowestQualityRequired;
    
    public ToolTypeEnum[] RequiredTools;
#endregion
#region -------------------- Methods --------------------
    public virtual void Interact()
    {
        // Override this in child classes
    }
#endregion
}}
