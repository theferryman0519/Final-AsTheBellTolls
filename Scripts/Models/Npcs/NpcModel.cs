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
using Atbt.Enum;
using Atbt.Object;

namespace Atbt.Npc {
public class NpcModel {
    
#region -------------------- Variables --------------------
    public NpcObject Npc;

    public int RelationshipPoints;
    public int HeartCount;

    public RelationshipStatusEnum RelationshipStatus;
    public RelationshipLevelEnum RelationshipLevel;

    public LocationObject Residence;
    public LocationObject Workplace;
#endregion
}}
