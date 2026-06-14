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
using Atbt.Time;

namespace Atbt.Invention {
public class InventionModel {
#region -------------------- Public Variables --------------------
    public int FullyCraftedDate;
    public int FullyTestedDate;
    
    public InventionStatusEnum CurrentStatus;
    public ShowcasePlacementEnum ShowcasePlacement;
    public SeasonTypeEnum FullyCraftedSeason;
    public SeasonTypeEnum FullyTestedSeason;
    
    public InventionObject Invention;
#endregion
#region -------------------- Public Methods --------------------

#endregion
}}