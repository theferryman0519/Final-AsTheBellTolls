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

namespace AsTheBellTolls.Player {
public class PlayerController : Singleton<PlayerController> {

#region -------------------- Serialized Variables --------------------
    [Header("Sub Controllers")]
    [SerializeField] private CameraSubController _cameraSubController;
    [SerializeField] private FamilySubController _familySubController;
    [SerializeField] private InputSubController _inputSubController;
    [SerializeField] private PlayerSubController _playerSubController;
    [SerializeField] private SkillTreeSubController _skillTreeSubController;
    [SerializeField] private TimeManipulationSubController _timeManipulationSubController;
#endregion
#region -------------------- Public Variables --------------------
    public CameraSubController Camera => _cameraSubController;
    public FamilySubController Family => _familySubController;
    public InputSubController Input => _inputSubController;
    public PlayerSubController Player => _playerSubController;
    public SkillTreeSubController SkillTree => _skillTreeSubController;
    public TimeManipulationSubController TimeManipulation => _timeManipulationSubController;
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
