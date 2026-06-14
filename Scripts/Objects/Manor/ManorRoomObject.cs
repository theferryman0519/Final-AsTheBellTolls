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
using Atbt.Item;
using Atbt.Quest;

namespace Atbt.Manor {
[CreateAssetMenu(menuName = "ATBT/Manor/Manor Room")]
public class ManorRoomObject : ScriptableObject {
#region -------------------- Public Variables --------------------
    public string Id;
    public string DisplayName;
        
    [TextArea]
    public string Description;

    public int CostNeeded;
    public int RestorationDuration;
    public int RestorationTimeManipulationDuration;
    
    public bool AccessibleByEdward;
    public bool AccessibleByOthers;
    
    public FloorLocationEnum FloorLocation;
    public RoomTierEnum RoomTier;
    
    public QuestObject QuestAssociation;
    
    public List<ItemNeeded> ItemsNeeded;
#endregion
}

public struct ItemNeeded
{
    public ItemObject Item;
    public int Count;
}}