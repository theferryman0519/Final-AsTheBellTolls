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
public class AccessibilityController : Singleton<AccessibilityController> {

#region -------------------- Serialized Variables --------------------

#endregion
#region -------------------- Public Variables --------------------
    [Header("UI Accessibility")]
    public float TextSize;
    public float TextSpeed;

    [Header("World Accessibility")]
    public float ScreenBrightness;
    public float CameraZoom;

    public bool VisualAssistance;

    [Header("Audio Accessibility")]
    public float MasterVolume;
    public float MusicVolume;
    public float SpeechVolume;
    public float EffectsVolume;
    public float AmbianceVolume;
    public float FootstepsVolume;
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
        CoreController.Inst.WriteLog(this.GetType().Name, $"Initializing the accessibility controller");

        CoreController.Inst.LoadingStepCompleted();
    }

    public void UpdateTextSize(float newSize)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the text size");

        // TODO
        // Update text size
        // Refresh UI
    }

    public void UpdateTextSpeed(float newSpeed)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the text speed");

        // TODO
        // Update text speed
    }

    public void UpdateScreenBrightness(float newBrightness)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the screen brightness");

        // TODO
        // Update screen brightness
        // Update scene light
    }

    public void UpdateCameraZoom(float newZoom)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the camera zoom");

        // TODO
        // Update camera zoom
        // Update scene camera
    }

    public void UpdateVisualAssistance(bool turnCuesOn)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the visual assistance cues");

        // TODO
        // Update visual assistance cues
        // Refresh UI
    }

    public void UpdateMasterVolume(float newVolume)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the master volume");

        // TODO
        // Update master volume
        // Update audio controller
    }

    public void UpdateMusicVolume(float newVolume)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the music volume");

        // TODO
        // Update music volume
        // Update audio controller
    }

    public void UpdateSpeechVolume(float newVolume)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the speech volume");

        // TODO
        // Update speech volume
        // Update audio controller
    }

    public void UpdateEffectsVolume(float newVolume)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the effects volume");

        // TODO
        // Update effects volume
        // Update audio controller
    }

    public void UpdateAmbianceVolume(float newVolume)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the ambiance volume");

        // TODO
        // Update ambiance volume
        // Update audio controller
    }

    public void UpdateFootstepsVolume(float newVolume)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the footsteps volume");

        // TODO
        // Update footsteps volume
        // Update audio controller
    }
#endregion
#region -------------------- Private Methods --------------------

#endregion
}}
