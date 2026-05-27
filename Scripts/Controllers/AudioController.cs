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
using Atbt.Audio;
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
    private float _currentMasterVolume = 1f;
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

    public void UpdateMasterVolume(float newVolume)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating all source volumes though the master");

        newVolume = Mathf.Clamp01(newVolume);

        if (Mathf.Approximately(_currentMasterVolume, 0f))
        {
            _currentMasterVolume = newVolume;
            return;
        }

        float multiplier = newVolume / _currentMasterVolume;

        UpdateMusicVolume(Mathf.Clamp01(MusicSource.volume * multiplier));
        UpdateSpeechVolume(Mathf.Clamp01(SpeechSource.volume * multiplier));
        UpdateAmbianceVolume(Mathf.Clamp01(AmbianceSource.volume * multiplier));
        UpdateEffectsVolume(Mathf.Clamp01(EffectsSource.volume * multiplier));
        UpdateFootstepsVolume(Mathf.Clamp01(FootstepsSource.volume * multiplier));

        currentMasterVolume = newVolume;
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
