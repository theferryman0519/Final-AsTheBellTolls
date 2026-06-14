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
using Atbt.Location;

namespace Atbt.Character {
public class NpcModel {
#region -------------------- Public Variables --------------------
    public int HeartCount;
    public int RelationshipPoints;

    public bool IsAvailableInGame;
    
    public ActivityStateEnum CurrentActivity;
    public RelationshipLevelEnum RelationshipLevel;
    public RelationshipStatusEnum RelationshipStatus;
    
    public LocationObject CurrentResidence;
    public RoutineObject CurrentRoutine;
    public NpcObject Npc;
#endregion
#region -------------------- Public Methods --------------------

#endregion
}}