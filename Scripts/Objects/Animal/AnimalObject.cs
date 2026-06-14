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
using Atbt.Time;

namespace Atbt.Animal {
[CreateAssetMenu(menuName = "ATBT/Animal/Animal")]
public class AnimalObject : ScriptableObject {
#region -------------------- Public Variables --------------------
    public string Id;
    public string DisplayName;
    
    [TextArea]
    public string Description;
    
    public int ByproductGrowthDuration;
    public int ByproductTimeManipulationDuration;
    public int MatureGrowthDuration;
    public int MatureTimeManipulationDuration;
    public int PurchasePrice;
    public int SalePriceBaby;
    public int SalePriceMature;
    
    public List<SeasonTypeEnum> ByproductSeasons;
    
    public List<ItemObject> Byproducts;
#endregion
}}