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
public class LocationController : Singleton<LocationController> {

#region -------------------- Serialized Variables --------------------
    [Header("Locations List")]
    [SerializeField] private List<LocationObject> LocationsList = new();
#endregion
#region -------------------- Public Variables --------------------
    [Header("Locations Dictionary")]
    public Dictionary<string, LocationObject> LocationsDict = new();
#endregion
#region -------------------- Private Variables --------------------

#endregion
#region -------------------- Initial Functions --------------------

#endregion
#region -------------------- Coroutines --------------------

#endregion
#region -------------------- Public Methods --------------------
    // Controls overworld spawning
    // Controls location unlocking
    // Controls fast travel

    public void InitializeController()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Initializing the location controller");

        LocationsDict.Clear();

        foreach (LocationObject obj in LocationsList)
        {
            LocationsDict.Add(obj.CorrespondingScene, obj);
        }

        CoreController.Inst.LoadingStepCompleted();
    }

    public LocationObject GetCurrentLocation()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Getting the current location object");

        string currentSceneName = CoreController.Inst.GetSceneName();

        return LocationsDict[currentSceneName];
    }
#endregion
#region -------------------- Private Methods --------------------

#endregion
}}
