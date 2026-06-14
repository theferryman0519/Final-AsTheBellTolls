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
public enum RelationshipLevelEnum {
#region -------------------- Enum List --------------------
    Stranger = 0,
    Acquaintance,
    Friendly,
    Friend,
    CloseFriend,
    Trusted,
    Betrothed,
    Spouse,
    SoulMate,
    Child,
#endregion
}}