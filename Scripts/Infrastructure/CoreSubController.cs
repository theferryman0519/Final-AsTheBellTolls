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
using AsTheBellTolls.Infrastructure.Core;

namespace AsTheBellTolls.Infrastructure {
public class CoreSubController : MonoBehaviour {

#region -------------------- Serialized Variables --------------------
    
#endregion
#region -------------------- Public Variables --------------------
    [Header("Loading Details")]
    public (int progress, int max) LoadingSteps;
	public (int progress, int max) LoadingImages;

    public bool IsLoaded;
    public bool IsFadeNeeded;

    [Header("Scene Names")]
    // Persistent Scenes
    public string Scene_Persistent00 = "Persistent_00_Controllers";

    // Main Scenes
    public string Scene_Main00 = "Main_00_Opening";
    public string Scene_Main01 = "Main_01_Title";
    public string Scene_Main02 = "Main_02_Continue";
    public string Scene_Main03 = "Main_03_New";
    public string Scene_Main04 = "Main_04_Loading";

    // Cinematics
    public string Scene_Cinematics00 = "Cinematics_00_Intro";

    // UI Pages
    public string Scene_Ui00 = "Ui_00_Creation";
    public string Scene_Ui01 = "Ui_01_Night";

    // Blackmere Market Square
    public string Scene_Blackmere_Square00 = "Blackmere_Square_00_Grounds";
    public string Scene_Blackmere_Square01 = "Blackmere_Square_01_Bank";
    public string Scene_Blackmere_Square02 = "Blackmere_Square_02_Store";
    public string Scene_Blackmere_Square03 = "Blackmere_Square_03_TownHall";

    // Blackmere Outskirts
    public string Scene_Blackmere_Outskirts00 = "Blackmere_Outskirts_00_Grounds";
    public string Scene_Blackmere_Outskirts01 = "Blackmere_Outskirts_01_Mines";
    public string Scene_Blackmere_Outskirts02 = "Blackmere_Outskirts_02_Cemetery";
    public string Scene_Blackmere_Outskirts03 = "Blackmere_Outskirts_03_Farms";

    // Blackmere Residential Lanes
    public string Scene_Blackmere_Lanes00 = "Blackmere_Lanes_00_Grounds";
    public string Scene_Blackmere_Lanes01 = "Blackmere_Lanes_01_Library";
    public string Scene_Blackmere_Lanes02 = "Blackmere_Lanes_02_School";
    public string Scene_Blackmere_Lanes03 = "Blackmere_Lanes_03_Museum";

    // Blackmwere River Ward
    public string Scene_Blackmere_Ward00 = "Blackmere_Ward_00_Grounds";
    public string Scene_Blackmere_Ward01 = "Blackmere_Ward_01_Fishery";
    public string Scene_Blackmere_Ward02 = "Blackmere_Ward_02_Inn";
    public string Scene_Blackmere_Ward03 = "Blackmere_Ward_03_InnUpstairs";

    // Blackmere Trades Quarter
    public string Scene_Blackmere_Quarter00 = "Blackmere_Quarter_00_Grounds";
    public string Scene_Blackmere_Quarter01 = "Blackmere_Quarter_01_Forge";
    public string Scene_Blackmere_Quarter02 = "Blackmere_Quarter_02_Woodworks";
    public string Scene_Blackmere_Quarter03 = "Blackmere_Quarter_03_Apothecary";
    public string Scene_Blackmere_Quarter04 = "Blackmere_Quarter_04_Tailor";
    public string Scene_Blackmere_Quarter05 = "Blackmere_Quarter_05_Studio";

    // Pendrelle Manor
    public string Scene_Blackmere_Manor00 = "Blackmere_Manor_00_Grounds";
    public string Scene_Blackmere_Manor01 = "Blackmere_Manor_01_Main";
    public string Scene_Blackmere_Manor02 = "Blackmere_Manor_02_Basement";
    public string Scene_Blackmere_Manor03 = "Blackmere_Manor_03_Upstairs";
    public string Scene_Blackmere_Manor04 = "Blackmere_Manor_04_Gardens";
    public string Scene_Blackmere_Manor05 = "Blackmere_Manor_05_Barn";

    // Morvanya
    public string Scene_Morvanya00 = "Morvanya_00_Forest";
    public string Scene_Morvanya01 = "Morvanya_01_Depths";
    public string Scene_Morvanya02 = "Morvanya_02_Mountain";
    public string Scene_Morvanya03 = "Morvanya_03_Lookout";
    public string Scene_Morvanya04 = "Morvanya_04_Lake";
#endregion
#region -------------------- Private Variables --------------------
    
#endregion
#region -------------------- Initial Functions --------------------
    
#endregion
#region -------------------- Coroutines --------------------
    
#endregion
#region -------------------- Public Methods --------------------
    public void Initialize()
	{
		WriteLog(this.GetType().Name, $"Initializing the sub controller.");

		LoadingSteps.max = Constant_Controller.Loading_StartUp;
		LoadingSteps = (0, Constant_Controller.Loading_StartUp);
		LoadingImages = (0, 0);
	}
#endregion
#region -------------------- Private Methods --------------------
    public void LoadingStepCompleted()
	{
		WriteLog(this.GetType().Name, $"Loading step has completed.");
		
		int newProgress = Mathf.Clamp(LoadingSteps.progress + 1, 0, LoadingSteps.max);
		LoadingSteps = (newProgress, LoadingSteps.max);

		if (newProgress == LoadingSteps.max)
		{
			IsLoaded = true;
		}

		else
		{
			IsLoaded = false;
		}
	}

    public void WriteLog(string fileName, string content)
	{
#if UNITY_EDITOR
		Debug.Log($"{fileName}: {content}");
#endif
	}

	public void WriteError(string fileName, string content)
	{
#if UNITY_EDITOR
		Debug.LogError($"{fileName}: {content}");
#endif
	}

	public void ChangeScene(string sceneName)
	{
		WriteLog(this.GetType().Name, $"Changing the scene to {sceneName}.");
		
		SceneManager.LoadScene(sceneName);
	}
#endregion
}}
