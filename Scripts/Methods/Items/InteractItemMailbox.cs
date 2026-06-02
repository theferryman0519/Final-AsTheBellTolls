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
public class InteractItemMailbox : InteractModel {
#region -------------------- Private Variables --------------------
    private QualityTypeEnum _lowestQualityRequired = QualityTypeEnum.Base;

    private ToolTypeEnum[] _requiredTools = { ToolTypeEnum.None};
#endregion
#region -------------------- Public Variables --------------------
    public QualityTypeEnum LowestQualityRequired => _lowestQualityRequired;
    
    public ToolTypeEnum[] RequiredTools => _requiredTools;
#endregion
#region -------------------- Methods --------------------
    public override void Interact()
    {
        // TODO
    }
#endregion
}}
