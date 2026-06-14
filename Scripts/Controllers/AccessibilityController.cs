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
public class AccessibilityController : Singleton<AccessibilityController> {
#region -------------------- Serialized Variables --------------------

#endregion
#region -------------------- Public Variables --------------------

#endregion
#region -------------------- Private Variables --------------------
    private float _cameraZoom;
    private float _screenBrightness;
    private float _textSpeed;
    private float _volumeAmbiance;
    private float _volumeEffects;
    private float _volumeFootsteps;
    private float _volumeMaster;
    private float _volumeMusic;
    private float _volumeSpeech;

    private int _textSize;

    private bool _showVisibleCues;
#endregion
#region -------------------- Initial Functions --------------------
    
#endregion
#region -------------------- Coroutines --------------------

#endregion
#region -------------------- Public Methods --------------------
    public void InitializeController()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Initializing the accessibility controller.");

        SetDefaults();
        CoreController.Inst.ControllerLoaded();
    }
    
    public float GetCameraZoom()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Returning the camera zoom.");

        return _cameraZoom;
    }
    
    public bool GetIfVisibleCues()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Returning if using visible cues.");

        return _showVisibleCues;
    }
    
    public float GetScreenBrightness()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Returning the screen brightness.");

        return _screenBrightness;
    }
    
    public int GetTextSize()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Returning the text size.");

        return _textSize;
    }
    
    public float GetTextSpeed()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Returning the text speed.");

        return _textSpeed;
    }
    
    public float GetVolume(AudioClipTypeEnum type)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Returning the volume of the audio {type}.");

        switch (type)
        {
            case AudioClipTypeEnum.Ambiance:
                return _volumeAmbiance;
            case AudioClipTypeEnum.Effects:
                return _volumeEffects;
            case AudioClipTypeEnum.Footsteps:
                return _volumeFootsteps;
            case AudioClipTypeEnum.Music:
                return _volumeMusic;
            case AudioClipTypeEnum.Speech:
                return _volumeSpeech;
            case AudioClipTypeEnum.Master:
            default:
                return _volumeMaster;
        }
    }
#endregion
#region -------------------- Private Methods --------------------
    private void SetDefaults()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Setting the default accessibility settings.");
        
        _cameraZoom = 5f;
        _screenBrightness = 1f;
        _textSpeed = 6f;
        _volumeAmbiance = 0.7f;
        _volumeEffects = 0.5f;
        _volumeFootsteps = 0.3f;
        _volumeMaster = 0.8f;
        _volumeMusic = 0.8f;
        _volumeSpeech = 0.7f;
        _textSize = 38;
        _showVisibleCues = true;
    }
#endregion
}}