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
using Atbt.Character;
using Atbt.Location;

namespace Atbt.Item {
[CreateAssetMenu(menuName = "ATBT/Item/Tool")]
public class ToolObject : ItemObject {
#region -------------------- Public Variables --------------------
    public bool CanBeUpgraded;
    
    public NpcObject AcquireCharacter;
    public LocationObject AcquireLocation;
    public ToolTypeEnum ToolType;
    
    public List<ItemObject> ItemsGathered;
#endregion
}}