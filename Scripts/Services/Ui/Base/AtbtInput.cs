// Main Dependencies
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// Game Dependencies
using Atbt.Controller;

namespace Atbt.Ui {
public class AtbtInput : MonoBehaviour {

#region -------------------- Serialized Variables --------------------
    [Header("Text Element")]
    [SerializeField] private TMP_InputField InputElement;
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
    public void SetListeners(UnityAction<string> selectAction, UnityAction<string> deselectAction, UnityAction<string> endEditAction)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the input listeners");

        InputElement.onSelect.RemoveAllListeners();
        InputElement.onDeselect.RemoveAllListeners();
        InputElement.onEndEdit.RemoveAllListeners();

        InputElement.onSelect.AddListener(selectAction);
        InputElement.onDeselect.AddListener(deselectAction);
        InputElement.onEndEdit.AddListener(endEditAction);
    }

    public string GetInput()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Getting the input text");

        return InputElement.text;
    }

    public void SetInput(string input)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the input text");

        InputElement.text = input;
    }
#endregion
#region -------------------- Private Methods --------------------
    
#endregion
}}
