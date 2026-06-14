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

namespace Atbt.Character {
public enum ActivityStateEnum {
#region -------------------- Enum List --------------------
    Idle = 0,
    Walking,
    Running,
    Talking,
    GivingGift,
    ReceivingGift,
    Eating,
    Drinking,
    Sitting,
    Loving,
    UsingItem,
    UsingTool,
    UsingInvention,
    Sleeping,
    Cinematic,
#endregion
}}