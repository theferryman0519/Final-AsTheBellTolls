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
using Atbt.Inventory;
using Atbt.Location;
using Atbt.Time;

namespace Atbt.Item {
[CreateAssetMenu(menuName = "ATBT/Item/Item")]
public class ItemObject : ScriptableObject {
#region -------------------- Public Variables --------------------
    public string Id;
    public string DisplayName;
        
    [TextArea]
    public string Description;

    public int PurchasePrice;
    public int ReplenishmentAmount;
    public int SpoilDuration;
    public int ValuePrice;
    
    public bool CanBeDifferentQualities;
    public bool CanBeGifted;
    public bool UsedInCooking;
    public bool UsedInCrafting;
    public bool UsedInInventions;
    public bool UsedInReplenishment;
    
    public InventoryLocationEnum InventoryLocation;
    public ItemTypeEnum Type;
    
    public List<LocationObject> SellingLocations;
    public List<SeasonTypeEnum> SellingSeasons;
#endregion
}}