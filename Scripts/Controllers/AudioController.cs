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
public class AudioController : Singleton<AudioController> {

#region -------------------- Serialized Variables --------------------
    [Header("Audio Sources")]
    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource SpeechSource;
    [SerializeField] private AudioSource AmbianceSource;
    [SerializeField] private AudioSource EffectsSource;
    [SerializeField] private AudioSource FootstepsSource;

    [Header("Audio Clip Holders")]
    [SerializeField] private AudioClipHolder MusicClipHolder;
    [SerializeField] private AudioClipHolder SpeechClipHolder;
    [SerializeField] private AudioClipHolder AmbianceClipHolder;
    [SerializeField] private AudioClipHolder EffectsClipHolder;
    [SerializeField] private AudioClipHolder FootstepsClipHolder;
#endregion
#region -------------------- Public Variables --------------------
    [Header("Audio Clip Dictionaries")]
    public Dictionary<string, AudioClip> MusicClips = new();
    public Dictionary<string, AudioClip> SpeechClips = new();
    public Dictionary<string, AudioClip> AmbianceClips = new();
    public Dictionary<string, AudioClip> EffectsClips = new();
    public Dictionary<string, AudioClip> FootstepsClips = new();
#endregion
#region -------------------- Private Variables --------------------

#endregion
#region -------------------- Initial Functions --------------------

#endregion
#region -------------------- Coroutines --------------------

#endregion
#region -------------------- Public Methods --------------------
    public void InitializeController()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Initializing the audio controller");

        MusicClips.Clear();
        SpeechClips.Clear();
        AmbianceClips.Clear();
        EffectsClips.Clear();
        FootstepsClips.Clear();

        SetDictionariesAsync();
    }

    public void UpdateMusicVolume(float newVolume)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the music source volume");

        MusicSource.volume = newVolume;
    }

    public void UpdateSpeechVolume(float newVolume)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the speech source volume");

        SpeechSource.volume = newVolume;
    }

    public void UpdateAmbianceVolume(float newVolume)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the ambiance source volume");

        AmbianceSource.volume = newVolume;
    }

    public void UpdateEffectsVolume(float newVolume)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the effects source volume");

        EffectsSource.volume = newVolume;
    }

    public void UpdateFootstepsVolume(float newVolume)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the footsteps source volume");

        FootstepsSource.volume = newVolume;
    }
#endregion
#region -------------------- Private Methods --------------------
    private async void SetDictionariesAsync()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Setting the audio clip dictionaries");

        await MusicClipHolder.SetAudioDictionaryAsync(AudioClipTypeEnum.Music);
        await SpeechClipHolder.SetAudioDictionaryAsync(AudioClipTypeEnum.Speech);
        await AmbianceClipHolder.SetAudioDictionaryAsync(AudioClipTypeEnum.Ambiance);
        await EffectsClipHolder.SetAudioDictionaryAsync(AudioClipTypeEnum.Effects);
        await FootstepsClipHolder.SetAudioDictionaryAsync(AudioClipTypeEnum.Footsteps);

        CoreController.Inst.LoadingStepCompleted();
    }
#endregion
}}
