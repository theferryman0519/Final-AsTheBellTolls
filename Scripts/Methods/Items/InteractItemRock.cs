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
using Atbt.Object;

namespace Atbt.Item {
public class InteractItemRock : InteractModel {
#region -------------------- Private Variables --------------------
    [SerializeField] private List<ItemObject> _rockItems;
    
    private QualityTypeEnum _lowestQualityRequired = QualityTypeEnum.Base;

    private ToolTypeEnum[] _requiredTools = { ToolTypeEnum.Pickaxe };
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
