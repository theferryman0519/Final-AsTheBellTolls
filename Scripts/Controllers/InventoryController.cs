// Main Dependencies
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// Game Dependencies
using Atbt.Core;
using Atbt.Enum;
using Atbt.Item;
using Atbt.Object;

namespace Atbt.Controller {
public class InventoryController : Singleton<InventoryController> {

#region -------------------- Serialized Variables --------------------

#endregion
#region -------------------- Public Variables --------------------
    [Header("Unlock Bools")]
    public bool IsInventoryUnlocked;
    public bool IsToolBeltUnlocked;
    public bool IsExtenderUnlocked;

    [Header("Inventory Slots")]
    public Dictionary<int, ItemModel> ToolBeltSlots = new();
    public Dictionary<int, ItemModel> InventorySlots = new();
#endregion
#region -------------------- Private Variables --------------------
    private int _maxStackCount = 999;
#endregion
#region -------------------- Initial Functions --------------------

#endregion
#region -------------------- Coroutines --------------------

#endregion
#region -------------------- Public Methods --------------------
    // Controls items in satchel
    // Controls tools in tool belt
    // Controls item stacks
    // Controls item quality stacks
    // Controls item transfers with storage units

    public void InitializeController()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Initializing the player inventory controller");

        CreateInventory();

        CoreController.Inst.LoadingStepCompleted();
    }

    public void AddToToolBelt(ItemModel item)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Adding {item.Item.DisplayName} to player tool belt");

        int firstEmptySlot = 0;

        for (int i = 0; i < ToolBeltSlots.Count; i++)
        {
            if (ToolBeltSlots[i] == null)
            {
                firstEmptySlot = i;
                break;
            }
        }

        if (firstEmptySlot > -1)
        {
            ToolBeltSlots[firstEmptySlot] = item;
        }

        else
        {
            // TODO: inventory full, remaining items could not be added
        }
    }

    public void AddToInventory(ItemModel item)
    {
        CoreController.Inst.WriteLog(GetType().Name, $"Adding {item.Item.DisplayName} to player inventory");

        int remaining = item.Count;

        foreach (ItemModel inventoryItem in InventorySlots.Values)
        {
            if (inventoryItem == null) { continue; }

            bool isSameItem = inventoryItem.Item.Id == item.Item.Id && inventoryItem.Quality == item.Quality && inventoryItem.Freshness == item.Freshness;

            if (!isSameItem) { continue; }

            int availableSpace = _maxStackCount - inventoryItem.Count;

            if (availableSpace <= 0) { continue; }

            int amountToAdd = Mathf.Min(remaining, availableSpace);

            inventoryItem.Count += amountToAdd;
            remaining -= amountToAdd;

            if (remaining <= 0) { return; }
        }

        while (remaining > 0)
        {
            int firstEmptySlot = -1;

            for (int i = 0; i < InventorySlots.Count; i++)
            {
                if (InventorySlots[i] == null)
                {
                    firstEmptySlot = i;
                    break;
                }
            }

            if (firstEmptySlot == -1)
            {
                // TODO: inventory full, remaining items could not be added
                return;
            }

            int stackCount = Mathf.Min(remaining, _maxStackCount);

            InventorySlots[firstEmptySlot] = new ItemModel
            {
                Item = item.Item,
                Count = stackCount,
                Quality = item.Quality,
                Freshness = item.Freshness
            };

            remaining -= stackCount;
        }
    }

    public void SortInventory(InventorySortEnum sortType)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Sorting the player inventory");

        List<ItemModel> sortedItems = InventorySlots.Values.Where(i => i != null).ToList();

        switch (sortType)
        {
            case InventorySortEnum.ZA:
                sortedItems = sortedItems.OrderByDescending(i => i.Item.DisplayName).ToList();
                break;
            case InventorySortEnum.HighLow:
                sortedItems = sortedItems.OrderByDescending(i => i.Count).ThenBy(i => i.Item.DisplayName).ToList();
                break;
            case InventorySortEnum.LowHigh:
                sortedItems = sortedItems.OrderBy(i => i.Count).ThenBy(i => i.Item.DisplayName).ToList();
                break;
            case InventorySortEnum.BaseCobalt:
                sortedItems = sortedItems.OrderBy(i => i.Item.DisplayName).ThenBy(i => i.Quality).ToList();
                break;
            case InventorySortEnum.CobaltBase:
                sortedItems = sortedItems.OrderBy(i => i.Item.DisplayName).ThenByDescending(i => i.Quality).ToList();
                break;
            case InventorySortEnum.Type:
                sortedItems = sortedItems.OrderBy(i => i.Item.ItemType).ThenBy(i => i.Item.DisplayName).ToList();
                break;
            case InventorySortEnum.AZ:
            default:
                sortedItems = sortedItems.OrderBy(i => i.Item.DisplayName).ToList();
                break;
        }

        for (int i = 0; i < InventorySlots.Count; i++)
        {
            InventorySlots[i] = i < sortedItems.Count ? sortedItems[i] : null;
        }
    }
#endregion
#region -------------------- Private Methods --------------------
    private void CreateInventory()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Creating the player inventory");

        ToolBeltSlots.Clear();
        InventorySlots.Clear();

        for (int tb = 0; tb < 8; tb++)
        {
            int index = tb;

            ToolBeltSlots.Add(index, null);
        }

        for (int i = 0; i < 32; i++)
        {
            int index = i;

            InventorySlots.Add(index, null);
        }
    }
#endregion
}}
