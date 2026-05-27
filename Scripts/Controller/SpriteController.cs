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

        if (TerrainSprites == null) { gameobject.AddComponent<SpriteVariants>(); }
        if (ExteriorSprites == null) { gameobject.AddComponent<SpriteVariants>(); }
        if (InteriorSprites == null) { gameobject.AddComponent<SpriteVariants>(); }
        if (CharacterSprites == null) { gameobject.AddComponent<SpriteVariants>(); }
        if (ItemSprites == null) { gameobject.AddComponent<SpriteVariants>(); }

        CoreController.Inst.LoadingStepCompleted();
    }
#endregion
#region -------------------- Private Methods --------------------

#endregion
}}
