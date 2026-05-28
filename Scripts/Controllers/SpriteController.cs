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
using Atbt.Core;
using Atbt.Enum;
using Atbt.Sprite;

namespace Atbt.Controller {
public class SpriteController : Singleton<SpriteController> {

#region -------------------- Serialized Variables --------------------
    [Header("Sprite Variants")]
    [SerializeField] private SpriteVariants TerrainSprites;
    [SerializeField] private SpriteVariants ExteriorSprites;
    [SerializeField] private SpriteVariants InteriorSprites;
    [SerializeField] private SpriteVariants CharacterSprites;
    [SerializeField] private SpriteVariants ItemSprites;
#endregion
#region -------------------- Public Variables --------------------
    [Header("Sprite Dictionaries")]
    public Dictionary<string, UnityEngine.Sprite> TerrainSpritesDict = new();
    public Dictionary<string, UnityEngine.Sprite> ExteriorSpritesDict = new();
    public Dictionary<string, UnityEngine.Sprite> InteriorSpritesDict = new();
    public Dictionary<string, UnityEngine.Sprite> CharacterSpritesDict = new();
    public Dictionary<string, UnityEngine.Sprite> ItemSpritesDict = new();
#endregion
#region -------------------- Private Variables --------------------

#endregion
#region -------------------- Initial Functions --------------------

#endregion
#region -------------------- Coroutines --------------------

#endregion
#region -------------------- Public Methods --------------------
    // Controls all sprites and tilemaps

    public void Initialize(Action continueAction = null)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Initializing the sprite controller");

        TerrainSpritesDict.Clear();
        ExteriorSpritesDict.Clear();
        InteriorSpritesDict.Clear();
        CharacterSpritesDict.Clear();
        ItemSpritesDict.Clear();

        SetDictionariesAsync();
    }
#endregion
#region -------------------- Private Methods --------------------
    private async void SetDictionariesAsync()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Setting the sprite dictionaries");

        await TerrainSprites.SetSpriteDictionaryAsync(SpriteTypeEnum.Terrain);
        await ExteriorSprites.SetSpriteDictionaryAsync(SpriteTypeEnum.Exterior);
        await InteriorSprites.SetSpriteDictionaryAsync(SpriteTypeEnum.Interior);
        await CharacterSprites.SetSpriteDictionaryAsync(SpriteTypeEnum.Character);
        await ItemSprites.SetSpriteDictionaryAsync(SpriteTypeEnum.Item);

        CoreController.Inst.LoadingStepCompleted();
    }
#endregion
}}
