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
public class AtbtDropdown : MonoBehaviour {

#region -------------------- Serialized Variables --------------------
    [Header("Dropdown Element")]
    [SerializeField] private TMP_Dropdown DropdownElement;
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
    public void SetListener(UnityAction<int> changeAction)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the dropdown actions");

        DropdownElement.onValueChanged.RemoveAllListeners();
        DropdownElement.onValueChanged.AddListener(changeAction);
    }

    public int GetValue()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Getting the dropdown value");

        return DropdownElement.value;
    }
#endregion
#region -------------------- Private Methods --------------------
    
#endregion
}}
