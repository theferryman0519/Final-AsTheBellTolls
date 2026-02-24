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
using AsTheBellTolls.Infrastructure.Core;

namespace AsTheBellTolls.Presentation {
public class PresentationController : Singleton<PresentationController> {

#region -------------------- Serialized Variables --------------------
    [Header("Sub Controllers")]
    [SerializeField] private AudioSubController _audioSubController;
    [SerializeField] private CinematicsSubController _cinematicsSubController;
    [SerializeField] private DialogueSubController _dialogueSubController;
    [SerializeField] private RiverSubController _riverSubController;
    [SerializeField] private UiSubController _uiSubController;
    [SerializeField] private VisualEffectsSubController _visualEffectsSubController;
#endregion
#region -------------------- Public Variables --------------------
    public AudioSubController Audio => _audioSubController;
    public CinematicsSubController Cinematics => _cinematicsSubController;
    public DialogueSubController Dialogue => _dialogueSubController;
    public RiverSubController River => _riverSubController;
    public UiSubController Ui => _uiSubController;
    public VisualEffectsSubController VisualEffects => _visualEffectsSubController;
#endregion
#region -------------------- Private Variables --------------------
    
#endregion
#region -------------------- Initial Functions --------------------
    
#endregion
#region -------------------- Coroutines --------------------
    
#endregion
#region -------------------- Public Methods --------------------
    
#endregion
#region -------------------- Private Methods --------------------
    
#endregion
}}
