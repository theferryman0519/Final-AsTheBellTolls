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
using Atbt.Controller;
using Atbt.Object;

namespace Atbt.Ui {
public class UiElementHud : MonoBehaviour {

#region -------------------- Serialized Variables --------------------
    [Header("Canvas Element")]
    [SerializeField] private Canvas CanvasElement;

    [Header("Text Objects")]
    [SerializeField] private AtbtText DateText;
    [SerializeField] private AtbtText TimeText;
    [SerializeField] private AtbtText LocationText;
    [SerializeField] private AtbtText InteractText;
#endregion
#region -------------------- Public Variables --------------------
    public Canvas MainCanvas => CanvasElement;
#endregion
#region -------------------- Private Variables --------------------
    
#endregion
#region -------------------- Initial Functions --------------------
    
#endregion
#region -------------------- Coroutines --------------------
    
#endregion
#region -------------------- Public Methods --------------------
    public void UpdateLocation()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the location text");

        LocationObject location = LocationController.Inst.GetCurrentLocation();
        string locationName = "Unknown Location";

        if (location != null)
        {
            locationName = locationName.DisplayName;
        }

        LocationText.SetText(locationName);
    }
#endregion
#region -------------------- Private Methods --------------------
    
#endregion
}}
