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
using Atbt.Item;
using Atbt.Time;

namespace Atbt.Character {
[CreateAssetMenu(menuName = "ATBT/Character/NPC")]
public class NpcObject : ScriptableObject {
#region -------------------- Public Variables --------------------
    public string Id;
    public string DisplayName;
    
    [TextArea]
    public string Description;

    public string FirstName;
    public string LastName;
    
    public int BirthdayDate;

    public bool IsMarriageCandidate;
    
    public GenderTypeEnum Gender;
    public SeasonTypeEnum BirthdaySeason;
    public ProfessionTypeEnum Profession;
    public SocialTraitEnum SocialTrait;
    public SocietalTraitEnum SocietalTrait;
    
    public List<ItemObject> GiftsDisliked;
    public List<ItemObject> GiftsHated;
    public List<ItemObject> GiftsLiked;
    public List<ItemObject> GiftsLoved;
    public List<ItemObject> GiftsNeutral;
    public List<PersonalityTrait> PersonalityTraits;
#endregion
}

public struct PersonalityTrait
{
    public PersonalityTraitEnum Trait;
    public int Score;
}}