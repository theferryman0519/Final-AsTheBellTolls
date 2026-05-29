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

        CoreController.Inst.LoadingStepCompleted();
    }
#endregion
#region -------------------- Private Methods --------------------
    private void RegisterGameInputActions()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Registering the game input actions");

        // TODO
        // Set up input actions
    }
#endregion
}}
