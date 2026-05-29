// Main Dependencies
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Game Dependencies
using Atbt.Core;

namespace Atbt.Controller {
public class InputController : Singleton<InputController> {

#region -------------------- Serialized Variables --------------------
    
#endregion
#region -------------------- Public Variables --------------------
    public event Action<Vector2> MoveInputChanged; // W / A / S / D

    public event Action InteractPressed; // Spacebar
    public event Action PausePressed; // Esc / Enter
    public event Action QuestPressed; // Q / Left Arrow
    public event Action InventionsPressed; // I / Right Arrow
    public event Action EdwardHelpPressed; // E
    public event Action PlayerMenuPressed; // P / Up Arrow
    public event Action SetManipulationPressed; // M / Down Arrow
    public event Action LookWavePressed; // L
    public event Action InventoryRowUpPressed; // [ / <
    public event Action InventoryRowDownPressed; // ] / >

    public Vector2 MoveInput => _moveInput;
#endregion
#region -------------------- Private Variables --------------------
    private GameInputAction _inputActions;

    private Vector2 _moveInput;
#endregion
#region -------------------- Initial Functions --------------------
    
#endregion
#region -------------------- Coroutines --------------------
    
#endregion
#region -------------------- Public Methods --------------------
    public void InitializeController()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Initializing the input controller");

        _inputActions = new GameInputActions();

        RegisterGameInputActions();
        SwitchToUi();

        CoreController.Inst.LoadingStepCompleted();
    }

    public void SwitchToGameplay()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Switching the input actions map to gameplay");

        DisableAllInputMaps();

        _inputActions.Gameplay.Enable();
    }

    public void SwitchToDialogue()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Switching the input actions map to dialogue");

        DisableAllInputMaps();

        _inputActions.Dialogue.Enable();
    }

    public void SwitchToUi()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Switching the input actions map to UI");

        DisableAllInputMaps();

        _inputActions.Ui.Enable();
    }
#endregion
#region -------------------- Private Methods --------------------
    private void RegisterGameInputActions()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Registering the game input actions");

        _inputActions.Gameplay.Move.performed += OnMoveInput;
        _inputActions.Gameplay.Move.canceled += OnMoveCanceled;

        _inputActions.Gameplay.Interact.performed += OnInteractInput;
        _inputActions.Gameplay.Pause.performed += OnPauseInput;
        _inputActions.Gameplay.Quest.performed += OnQuestInput;
        _inputActions.Gameplay.Inventions.performed += OnInventionsInput;
        _inputActions.Gameplay.EdwardHelp.performed += OnEdwardHelpInput;
        _inputActions.Gameplay.PlayerMenu.performed += OnPlayerMenuInput;
        _inputActions.Gameplay.SetManipulation.performed += OnSetManipulationInput;
        _inputActions.Gameplay.LookWave.performed += OnLookWaveInput;
        _inputActions.Gameplay.InventoryRowUp.performed += OnInventoryRowUpInput;
        _inputActions.Gameplay.InventoryRowDown.performed += OnInventoryRowDownInput;
    }

    private void DisableAllInputMaps()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Disabling all input action maps");

        _inputActions.Gameplay.Disable();
        _inputActions.Dialogue.Disable();
        _inputActions.Ui.Disable();

        _moveInput = Vector2.zero;
        MoveInputChanged?.Invoke(_moveInput);
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Inputting move from key press");

        Vector2 input = context.ReadValue<Vector2>();

        if (Mathf.Abs(input.x) < ConstantController.Threshold_MoveInputDeadzone)
        {
            input.x = 0;
        }

        if (Mathf.Abs(input.y) < ConstantController.Threshold_MoveInputDeadzone)
        {
            input.y = 0;
        }

        _moveInput = input;
        MoveInputChanged?.Invoke(_moveInput);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Canceling the move from key press");

        _moveInput = Vector2.zero;
        MoveInputChanged?.Invoke(_moveInput);
    }

    private void OnInteractInput(InputAction.CallbackContext context)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Inputting interact from key press");

        InteractPressed?.Invoke();
    }

    private void OnPauseInput(InputAction.CallbackContext context)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Inputting pause menu from key press");

        PausePressed?.Invoke();
    }

    private void OnQuestInput(InputAction.CallbackContext context)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Inputting quest menu from key press");

        QuestPressed?.Invoke();
    }

    private void OnInventionsInput(InputAction.CallbackContext context)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Inputting inventions menu from key press");

        InventionsPressed?.Invoke();
    }

    private void OnEdwardHelpInput(InputAction.CallbackContext context)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Inputting Edward help menu from key press");

        EdwardHelpPressed?.Invoke();
    }

    private void OnPlayerMenuInput(InputAction.CallbackContext context)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Inputting player menu from key press");

        PlayerMenuPressed?.Invoke();
    }

    private void OnSetManipulationInput(InputAction.CallbackContext context)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Inputting set time manipulation menu from key press");

        SetManipulationPressed?.Invoke();
    }

    private void OnLookWaveInput(InputAction.CallbackContext context)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Inputting player wave from key press");

        LookWavePressed?.Invoke();
    }

    private void OnInventoryRowUpInput(InputAction.CallbackContext context)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Inputting toggling inventory row up from key press");

        InventoryRowUpPressed?.Invoke();
    }

    private void OnInventoryRowDownInput(InputAction.CallbackContext context)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Inputting toggling inventory row down from key press");

        InventoryRowDownPressed?.Invoke();
    }
#endregion
}}
