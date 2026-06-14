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
using Atbt.Item;

namespace Atbt.Quest {
[CreateAssetMenu(menuName = "ATBT/Quest/Quest")]
public class QuestObject : ScriptableObject {
#region -------------------- Public Variables --------------------
    public string Id;
    public string DisplayName;
        
    [TextArea]
    public string Description;
    
    public int RewardCost;

    public NpcObject Owner;
    
    public List<ItemReward> RewardItems;
#endregion
}

public struct ItemReward
{
    public ItemObject Item;
    public int Count;
}}