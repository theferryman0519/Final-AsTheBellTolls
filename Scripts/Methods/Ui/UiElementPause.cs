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
using Atbt.Enum;

namespace Atbt.Ui {
public class UiElementPause : MonoBehaviour {

#region -------------------- Serialized Variables --------------------
    [Header("Canvas Element")]
    [SerializeField] private Canvas CanvasElement;

    [Header("Menu Pages")]
    [SerializeField] private GameObject PausePage;
    [SerializeField] private GameObject SettingsPage;
    [SerializeField] private GameObject ControlsPage;
    [SerializeField] private GameObject BugReportPage;
#endregion
#region -------------------- Public Variables --------------------
    public Canvas MainCanvas => CanvasElement;
#endregion
#region -------------------- Private Variables --------------------
    
#endregion
#region -------------------- Initial Functions --------------------
    
#endregion
#region -------------------- Coroutines --------------------
    
#endregion
#region -------------------- Public Methods --------------------
    public void ShowPage(PauseMenuTypeEnum pageType)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Showing the appropriate pause menu page");

        PausePage.SetActive(pageType == PauseMenuTypeEnum.Pause);
        SettingsPage.SetActive(pageType == PauseMenuTypeEnum.Settings);
        ControlsPage.SetActive(pageType == PauseMenuTypeEnum.Controls);
        BugReportPage.SetActive(pageType == PauseMenuTypeEnum.Report);
    }
#endregion
#region -------------------- Private Methods --------------------
    
#endregion
}}
