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

namespace Atbt.Item {
[CreateAssetMenu(menuName = "ATBT/Item/Tonic")]
public class TonicObject : RecipeItemObject {
#region -------------------- Public Variables --------------------
    public int BuffDuration;
    
    public float BuffAmount;
    
    public BuffTypeEnum BuffType;
#endregion
}}