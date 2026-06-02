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

namespace Atbt.Location {
public class InteractModel : MonoBehaviour {
    
#region -------------------- Variables --------------------
    public ToolTypeEnum RequiredTool;
    public QualityTypeEnum LowestQualityRequired;

    public Action InteractAction;
#endregion
}}
