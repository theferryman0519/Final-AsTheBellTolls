// Main Dependencies
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// Game Dependencies
using Atbt.Controller;
using Atbt.Enum;

namespace Atbt.Audio {
public class AudioClipHolder : MonoBehaviour {

#region -------------------- Serialized Variables --------------------
    [Header("Audio Clip Lists")]
    [SerializeField] private List<AudioClip> ClipList = new();
    [SerializeField] private List<string> ClipTitleList = new();
#endregion
#region -------------------- Public Variables --------------------
    
#endregion
#region -------------------- Private Variables --------------------
    private int _maxConcurrentDownloads = 5;
#endregion
#region -------------------- Initial Functions --------------------
    
#endregion
#region -------------------- Coroutines --------------------
    
#endregion
#region -------------------- Public Methods --------------------
    public async Task SetAudioDictionaryAsync(AudioClipTypeEnum dictSelection)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Setting the specific audio clip dictionaries");

        SemaphoreSlim semaphore = new SemaphoreSlim(_maxConcurrentDownloads, _maxConcurrentDownloads);
        List<Task> tasks = new List<Task>(ClipList.Count);

        for (int i = 0; i < ClipList.Count; i++)
        {
            int index = i;

            await semaphore.WaitAsync();
            tasks.Add(ProcessAudioDictionaryAsync(dictSelection, index, semaphore));
        }

        await Task.WhenAll(tasks);
    }
#endregion
#region -------------------- Private Methods --------------------
    private async Task ProcessAudioDictionaryAsync(AudioClipTypeEnum dictSelection, int index, SemaphoreSlim semaphore)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Processing the audio clip dictionaries");

        try
        {
            switch (dictSelection)
            {
                case AudioClipTypeEnum.Music:
                    AudioController.Inst.MusicClips.Add(ClipTitleList[index], ClipList[index]);
                    break;
                case AudioClipTypeEnum.Speech:
                    AudioController.Inst.SpeechClips.Add(ClipTitleList[index], ClipList[index]);
                    break;
                case AudioClipTypeEnum.Ambiance:
                    AudioController.Inst.AmbianceClips.Add(ClipTitleList[index], ClipList[index]);
                    break;
                case AudioClipTypeEnum.Effects:
                    AudioController.Inst.EffectsClips.Add(ClipTitleList[index], ClipList[index]);
                    break;
                case AudioClipTypeEnum.Footsteps:
                    AudioController.Inst.FootstepsClips.Add(ClipTitleList[index], ClipList[index]);
                    break;
                default:
                    break;
            }
        }

        catch
        {
            CoreController.Inst.WriteError(this.GetType().Name, $"Could not process audio clip {ClipTitleList[index]}");
        }

        finally
        {
            semaphore.Release();
        }
    }
#endregion
}}
