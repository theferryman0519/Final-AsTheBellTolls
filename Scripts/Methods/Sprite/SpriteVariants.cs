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
using Atbt.Controller;

namespace Atbt.Sprite {
public class SpriteVariants : MonoBehaviour {

#region -------------------- Serialized Variables --------------------
    [Header("Sprites List")]
    [SerializeField] private List<Sprite> SpriteList = new();
#endregion
#region -------------------- Public Variables --------------------
    
#endregion
#region -------------------- Private Variables --------------------
    private int _maxConcurrentDownloads = 30;
#endregion
#region -------------------- Initial Functions --------------------
    
#endregion
#region -------------------- Coroutines --------------------
    
#endregion
#region -------------------- Public Methods --------------------
    public async Task SetSpriteDictionaryAsync(int dictSelection)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Setting the specific sprite dictionaries");

        SemaphoreSlim semaphore = new SemaphoreSlim(_maxConcurrentDownloads, _maxConcurrentDownloads);
        List<Task> tasks = new List<Task>(SpriteList.Count);

        foreach (Sprite sprite in SpriteList)
        {
            await semaphore.WaitAsync();
            tasks.Add(ProcessSpriteDictionaryAsync(dictSelection, sprite, semaphore));

            switch (dictSelection)
            {
                case 0:
                    SpriteController.Inst.TerrainSpritesDict.Add(sprite.name, sprite);
                    break;
                case 0:
                    SpriteController.Inst.ExteriorSpritesDict.Add(sprite.name, sprite);
                    break;
                case 0:
                    SpriteController.Inst.InteriorSpritesDict.Add(sprite.name, sprite);
                    break;
                case 0:
                    SpriteController.Inst.CharacterSpritesDict.Add(sprite.name, sprite);
                    break;
                case 0:
                    SpriteController.Inst.ItemSpritesDict.Add(sprite.name, sprite);
                    break;
            }
        }

        await Task.WhenAll(tasks);
    }
#endregion
#region -------------------- Private Methods --------------------
    private async Task ProcessSpriteDictionaryAsync(int dictSelection, Sprite sprite, SemaphoreSlim semaphore)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Processing the specific sprite dictionaries");

        try
        {
            switch (dictSelection)
            {
                case 0:
                    SpriteController.Inst.TerrainSpritesDict.Add(sprite.name, sprite);
                    break;
                case 0:
                    SpriteController.Inst.ExteriorSpritesDict.Add(sprite.name, sprite);
                    break;
                case 0:
                    SpriteController.Inst.InteriorSpritesDict.Add(sprite.name, sprite);
                    break;
                case 0:
                    SpriteController.Inst.CharacterSpritesDict.Add(sprite.name, sprite);
                    break;
                case 0:
                    SpriteController.Inst.ItemSpritesDict.Add(sprite.name, sprite);
                    break;
            }
        }

        catch
        {
            CoreController.Inst.WriteError(this.GetType().Name, $"Could not process sprite {sprite.name}");
        }

        finally
        {
            semaphore.Release();
        }
    }
#endregion
}}