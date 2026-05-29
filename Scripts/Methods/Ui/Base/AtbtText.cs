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
public class AtbtText : MonoBehaviour {

#region -------------------- Serialized Variables --------------------
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
    public void SetText(string content)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the text content");

        TextElement.text = content;
    }
#endregion
#region -------------------- Private Methods --------------------
    
#endregion
}}
