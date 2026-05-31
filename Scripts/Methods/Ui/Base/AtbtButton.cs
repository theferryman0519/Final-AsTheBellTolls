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

namespace Atbt.Ui {
public class AtbtButton : MonoBehaviour {

#region -------------------- Serialized Variables --------------------
    [Header("Button Element")]
    [SerializeField] private Button ButtonElement;
    [SerializeField] private CanvasGroup CanvasElement;
    [SerializeField] private CanvasGroup HighlightElement;
    
    [Header("Text Element")]
    [SerializeField] private TMP_Text TextElement;
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
    public void SetAction(Action action = null)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the button action");

        ButtonElement.onClick.RemoveAllListeners();
        ButtonElement.onClick.AddListener(() =>
        {
            action?.Invoke();
        });
    }
    
    public void SetInteractability(bool isActive)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the button interactability");

        ButtonElement.interactable = isActive;
    }
    
    public void SetText(string content)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the text content");

        TextElement.text = content;
    }

    public void SetVisibility(bool isActive)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the button visibility");

        CanvasElement.alpha = isActive ? 1f : 0f;
    }

    public void SetHighlight(bool isActive)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the button highlight visibility");

        HighlightElement.alpha = isActive ? 1f : 0f;
    }
#endregion
#region -------------------- Private Methods --------------------
    
#endregion
}}
