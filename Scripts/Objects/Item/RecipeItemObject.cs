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
[CreateAssetMenu(menuName = "ATBT/Item/Recipe Item")]
public class RecipeItemObject : ItemObject {
#region -------------------- Public Variables --------------------
    public int MakingDuration;
    public int MakingTimeManipulationDuration;
    
    public bool CanBeDyed;
    public bool UsedInDecorating;
    public bool UsedInMealEvents;
    
    public RecipeCreationPlaceEnum CreationPlace;
    
    public List<ItemNeeded> ItemsNeeded;
#endregion
}

public struct ItemNeeded
{
    public ItemObject Item;
    public int Count;
}}