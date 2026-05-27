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
    public Dictionary<string, Sprite> TerrainSpritesDict;
    public Dictionary<string, Sprite> ExteriorSpritesDict;
    public Dictionary<string, Sprite> InteriorSpritesDict;
    public Dictionary<string, Sprite> CharacterSpritesDict;
    public Dictionary<string, Sprite> ItemSpritesDict;
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

        SetDictionaries();
    }
#endregion
#region -------------------- Private Methods --------------------
    private async void SetDictionaries()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Setting the sprite dictionaries");

        await TerrainSprites.SetSpriteDictionaryAsync(0);
        await ExteriorSprites.SetSpriteDictionaryAsync(1);
        await InteriorSprites.SetSpriteDictionaryAsync(2);
        await CharacterSprites.SetSpriteDictionaryAsync(3);
        await ItemSprites.SetSpriteDictionaryAsync(4);

        CoreController.Inst.LoadingStepCompleted();
    }
#endregion
}}
