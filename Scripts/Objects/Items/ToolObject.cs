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

namespace Atbt.Object {
[CreateAssetMenu(menuName = "Objects/Tool")]
public class ToolObject : ScriptableObject {
    public ItemObject Item;

    public int StaminaUsed;
    public int UsageAmount;
    public int ObtainedAmount;

    public bool IsUpgradable;

    public ToolTypeEnum ToolType;
    public InventoryLocationEnum InventoryLocation;

    public List<ItemObject> ItemsObtained;
}}
