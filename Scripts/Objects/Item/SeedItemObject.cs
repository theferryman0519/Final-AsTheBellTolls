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
[CreateAssetMenu(menuName = "ATBT/Item/Seed Item")]
public class SeedItemObject : ItemObject {
#region -------------------- Public Variables --------------------
    public int GrowthDuration;
    public int GrowthTimeManipulationDuration;
#endregion
}}