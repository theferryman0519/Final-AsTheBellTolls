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

namespace Atbt.Object {
[CreateAssetMenu(menuName = "Objects/Npc")]
public class NpcObject : ScriptableObject {
    public string Id;
    public string FirstName;
    public string LastName;

    public int BirthdayDate;

    public bool IsMarriageCandidate;

    public SeasonTypeEnum BirthdaySeason;
    public GenderTypeEnum Gender;
    public ProfessionTypeEnum Profession;

    public List<ItemObject> LovedGifts;
    public List<ItemObject> LikedGifts;
    public List<ItemObject> NeutralGifts;
    public List<ItemObject> DislikedGifts;
    public List<ItemObject> HatedGifts;
}}
