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
using Atbt.Core;
using Atbt.Object;

namespace Atbt.Controller {
public class ItemController : Singleton<ItemController> {

#region -------------------- Serialized Variables --------------------
    [Header("Item Objects")]
    [SerializeField] private List<ItemObject> ArtifactsList = new();
    [SerializeField] private List<ItemObject> BooksList = new();
    [SerializeField] private List<ItemObject> ByproductsList = new();
    [SerializeField] private List<ItemObject> ClothingList = new();
    [SerializeField] private List<ItemObject> CropsList = new();
    [SerializeField] private List<ItemObject> DecorList = new();
    [SerializeField] private List<ItemObject> DiscsList = new();
    [SerializeField] private List<ItemObject> DrinksList = new();
    [SerializeField] private List<ItemObject> FabricationsList = new();
    [SerializeField] private List<ItemObject> FishList = new();
    [SerializeField] private List<ItemObject> FlowersList = new();
    [SerializeField] private List<ItemObject> FurnitureList = new();
    [SerializeField] private List<ItemObject> GemstonesList = new();
    [SerializeField] private List<ItemObject> HerbsList = new();
    [SerializeField] private List<ItemObject> InventionsList = new();
    [SerializeField] private List<ItemObject> MealsList = new();
    [SerializeField] private List<ItemObject> OresList = new();
    [SerializeField] private List<ItemObject> SeedsList = new();
    [SerializeField] private List<ItemObject> StonesList = new();
    [SerializeField] private List<ItemObject> TonicsList = new();
    [SerializeField] private List<ItemObject> ToolsList = new();
    [SerializeField] private List<ItemObject> WoodsList = new();
#endregion
#region -------------------- Public Variables --------------------

#endregion
#region -------------------- Private Variables --------------------

#endregion
#region -------------------- Initial Functions --------------------

#endregion
#region -------------------- Coroutines --------------------

#endregion
#region -------------------- Public Methods --------------------
    // Controls central item lookup

    public void InitializeController()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Initializing the item controller");

        CoreController.Inst.LoadingStepCompleted();
    }
#endregion
#region -------------------- Private Methods --------------------

#endregion
}}
