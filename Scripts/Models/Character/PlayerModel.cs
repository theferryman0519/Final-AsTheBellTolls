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
using Atbt.Location;
using Atbt.Time;

namespace Atbt.Character {
public class PlayerModel {
#region -------------------- Public Variables --------------------
    public string CustomName;
    public string HairStyleId;
    
    public int CurrentDate;
    public int CurrentYear;
    public int CurrentWealth;

    public GenderTypeEnum Gender;
    public CharacterEyeColorEnum EyeColor;
    public CharacterHairColorEnum HairColor;
    public CharacterSkinToneEnum SkinTone;
    public CharacterClothingColorEnum HatColor;
    public CharacterClothingColorEnum TopColor;
    public CharacterClothingColorEnum BottomColor;
    public CharacterClothingColorEnum ShoesColor;
    public CharacterClothingColorEnum GlassesColor;
    public CharacterClothingColorEnum GlovesColor;
    public SeasonTypeEnum CurrentSeason;
    public WeekdayTypeEnum CurrentWeekday;
    public RelationshipStatusEnum RelationshipStatus;
    
    public ItemObject Hat;
    public ItemObject Top;
    public ItemObject Bottom;
    public ItemObject Shoes;
    public ItemObject Glasses;
    public ItemObject Gloves;
#endregion
#region -------------------- Public Methods --------------------

#endregion
}}