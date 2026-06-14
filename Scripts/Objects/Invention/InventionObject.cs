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

namespace Atbt.Invention {
[CreateAssetMenu(menuName = "ATBT/Invention/Invention")]
public class InventionObject : ScriptableObject {
#region -------------------- Public Variables --------------------
    public string Id;
    public string DisplayName;
        
    [TextArea]
    public string Description;

    public string TreeSlot;

    public int CraftingDuration;
    public int CraftingTimeManipulationDuration;
    public int DifficultyLevel;
    public int TestingDuration;
    public int TestingTimeManipulationDuration;
    public int ValuePrice;

    public bool CanBeGifted;
    public bool CanBeSold;
    public bool ShowcaseSubmittable;
    public bool WillImpressRival;
    
    public (int Low, int High) ShowcaseScoreRange;
    
    public AdditionTypeEnum AdditionType;
    public SocietalReactionEnum SocietalReaction;
    public UnlockTypeEnum UnlockType;
    public InventionUsageEnum Usage;
    
    public ItemObject RequiredGemstone;
    
    public List<RequiredResource> RequiredResources;
#endregion
}

public struct RequiredResource {
    public ItemObject Item;
    public int Count;
}}