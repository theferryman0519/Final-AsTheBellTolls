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
    
#region -------------------- Variables --------------------
    public QualityTypeEnum LowestQualityRequired;
    
    public ToolTypeEnum[] RequiredTools;

    public virtual void Interact()
    {
        // Override this in child classes
    }
#endregion
}}
