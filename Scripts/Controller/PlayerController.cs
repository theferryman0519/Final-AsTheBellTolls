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

namespace Atbt.Controller {
public class PlayerController : Singleton<PlayerController> {

#region -------------------- Serialized Variables --------------------
    [Header("Player Movement")]
    [SerializeField] private PlayerMovement Movement;

    [Header("Player Stamina")]
    [SerializeField] private PlayerStamina Stamina;
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
    // Controls player movement
    // Controls player stamina
    // Controls player interactions
    // Controls player tool usage
    // Controls state of the player

    public void Initialize(Action continueAction = null)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Initializing the player controller");

        if (Movement == null) { gameobject.AddComponent<PlayerMovement>(); }
        if (Stamina == null) { gameobject.AddComponent<PlayerStamina>(); }

        CoreController.Inst.LoadingStepCompleted();
    }
#endregion
#region -------------------- Private Methods --------------------

#endregion
}}
