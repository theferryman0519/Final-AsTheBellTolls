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
[CreateAssetMenu(menuName = "ATBT/Item/Fish")]
public class FishObject : IngredientItemObject {
#region -------------------- Public Variables --------------------
    public FishLocationEnum LocationType;
    public FishSchoolingEnum SchoolingType;
#endregion
}}