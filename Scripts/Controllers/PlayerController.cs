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
using Atbt.Player;

namespace Atbt.Controller {
public class PlayerController : Singleton<PlayerController> {

#region -------------------- Serialized Variables --------------------
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

    public void InitializeController()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Initializing the player controller");

        if (Stamina == null) { gameObject.AddComponent<PlayerStamina>(); }

        CoreController.Inst.LoadingStepCompleted();
    }

    public void TakeNap()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Player is starting to take a nap");

        // TODO
        // Fade screen to black

        TimeController.Inst.AdvanceNapTime();

        // TODO
        // Restore stamina by +60
        // Fade screen back
    }

    public void GoToBed()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Player is going to bed to end their day");

        // TODO
        // Fade screen to black

        TimeController.Inst.StartEndOfDay();
    }
#endregion
#region -------------------- Private Methods --------------------

#endregion
}}
