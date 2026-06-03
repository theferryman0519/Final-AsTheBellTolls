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
public class AtbtSlider : MonoBehaviour {

#region -------------------- Serialized Variables --------------------
    [Header("Slider Element")]
    [SerializeField] private Slider SliderElement;
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
    public void SetAmount(float amount)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the slider amount");

        SliderElement.value = amount;
    }
#endregion
#region -------------------- Private Methods --------------------
    
#endregion
}}
