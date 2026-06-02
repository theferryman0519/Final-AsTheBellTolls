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
using Atbt.Location;
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
    private InteractModel _currentInteractable;
#endregion
#region -------------------- Initial Functions --------------------
    void OnDisable()
    {
        InputController.Inst.InteractPressed -= Interact;
    }
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

        if (Stamina == null) { Stamina = gameObject.AddComponent<PlayerStamina>(); }

        InputController.Inst.InteractPressed += Interact;

        CoreController.Inst.LoadingStepCompleted();
    }

    public void GetPlayerStamina()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Getting the player stamina");

        // TODO
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

    public void Interact()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Player is interacting with object");

        if (_currentInteractable == null)
        {
            return;
        }

        _currentInteractable.InteractAction?.Invoke();
    }
#endregion
#region -------------------- Private Methods --------------------
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Player has triggered an action");

        if (trigger.TryGetComponent(out InteractModel interactable))
        {
            _currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Player has left a trigger");

        if (trigger.TryGetComponent(out InteractModel interactable) && interactable == _currentInteractable)
        {
            _currentInteractable = null;
        }
    }
#endregion
}}
