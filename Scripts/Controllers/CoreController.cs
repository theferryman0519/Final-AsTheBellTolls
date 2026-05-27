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
public class CoreController : Singleton<CoreController> {

#region -------------------- Serialized Variables --------------------

#endregion
#region -------------------- Public Variables --------------------
    [Header("Loading")]
    public (int Progress, int Max) LoadingSteps;

    public bool IsLoaded;

    [Header("Persistent Scenes")]
    public string Scene_Persistent00 = "Persistent_00_Controllers";

    [Header("Main Scenes")]
    public string Scene_Main00 = "Main_00_Studio";
    public string Scene_Main01 = "Main_01_Opening";
    public string Scene_Main02 = "Main_02_Title";
    public string Scene_Main03 = "Main_03_Load";
    public string Scene_Main04 = "Main_04_Credits";

    [Header("Intro Scenes")]
    public string Scene_Intro00 = "Intro_00_Carriage";
    public string Scene_Intro01 = "Intro_01_WindingBanksInn";
    public string Scene_Intro02 = "Intro_02_TownSquare";
    public string Scene_Intro03 = "Intro_03_PendrelleManor";

    [Header("Exterior Scenes")]
    public string Scene_Exterior00 = "Exterior_00_Blackmere";
    public string Scene_Exterior01 = "Exterior_01_PendrelleManor";
    public string Scene_Exterior02 = "Exterior_02_MercerFarms";
    public string Scene_Exterior03 = "Exterior_03_MemorialHill";
    public string Scene_Exterior04 = "Exterior_04_AshfallMines";
    public string Scene_Exterior05 = "Exterior_05_GraythorneLake";
    public string Scene_Exterior06 = "Exterior_06_SteelridgeWatch";
    public string Scene_Exterior07 = "Exterior_07_IronveilPeak";
    public string Scene_Exterior08 = "Exterior_08_GloamwoodForest";
    public string Scene_Exterior09 = "Exterior_09_GloamwoodDepths";

    [Header("Interior Scenes")]
    public string Scene_Interior00 = "Interior_00_PendrelleManor";
    public string Scene_Interior01 = "Interior_01_MercerFarms";
    public string Scene_Interior02 = "Interior_02_MemorialHill";
    public string Scene_Interior03 = "Interior_03_1ResidentLane";
    public string Scene_Interior04 = "Interior_04_2ResidentLane";
    public string Scene_Interior05 = "Interior_05_3ResidentLane";
    public string Scene_Interior06 = "Interior_06_4ResidentLane";
    public string Scene_Interior07 = "Interior_07_5ResidentLane";
    public string Scene_Interior08 = "Interior_08_6ResidentLane";
    public string Scene_Interior09 = "Interior_09_PublicLibrary";
    public string Scene_Interior10 = "Interior_10_PublicSchool";
    public string Scene_Interior11 = "Interior_11_HallOfWonder";
    public string Scene_Interior12 = "Interior_12_RiverbendFishery";
    public string Scene_Interior13 = "Interior_13_WindingBanksInn";
    public string Scene_Interior14 = "Interior_14_BlackmereBank";
    public string Scene_Interior15 = "Interior_15_BlackmereTower";
    public string Scene_Interior16 = "Interior_16_BlackmereTownHall";
    public string Scene_Interior17 = "Interior_17_BlackmereGeneralStore";
    public string Scene_Interior18 = "Interior_18_IronveilForge";
    public string Scene_Interior19 = "Interior_19_KleinWoodworks";
    public string Scene_Interior20 = "Interior_20_RainAndHale";
    public string Scene_Interior21 = "Interior_21_ThreadAndThimble";
    public string Scene_Interior22 = "Interior_22_WeissDesignStudio";


    [Header("Festival Scenes")]
    public string Scene_Festival00 = "Festival_00_RiverRemembranceDay";
    public string Scene_Festival01 = "Festival_01_SeedwakeBrunch";
    public string Scene_Festival02 = "Festival_02_BlackmereTownFair";
    public string Scene_Festival03 = "Festival_03_FunInTheSunFestival";
    public string Scene_Festival04 = "Festival_04_GrandShowcase";
    public string Scene_Festival05 = "Festival_05_HarvestSupper";
    public string Scene_Festival06 = "Festival_06_HollowMoonNight";
    public string Scene_Festival07 = "Festival_07_TollOfHearths";
    public string Scene_Festival08 = "Festival_08_SnowBellsEve";
    public string Scene_Festival09 = "Festival_09_TroutTrials";
    public string Scene_Festival10 = "Festival_10_CookingWithTheTwins";
    public string Scene_Festival11 = "Festival_11_SalmonRun";
    public string Scene_Festival12 = "Festival_12_AuroraWatch";

    [Header("Summary Scenes")]
    public string Scene_Summary00 = "Summary_00_EndOfDay";
    public string Scene_Summary01 = "Summary_01_AutoSave";
#endregion
#region -------------------- Private Variables --------------------

#endregion
#region -------------------- Initial Functions --------------------
    void Start()
    {
        // // FOR TESTING
        // PlayerPrefs.DeleteAll();
        // PlayerPrefs.Save();

        IsLoaded = false;
        SceneManager.LoadSceneAsync(Scene_Main00, LoadSceneMode.Single);

        InitializeController();
    }
#endregion
#region -------------------- Coroutines --------------------

#endregion
#region -------------------- Public Methods --------------------
    public void InitializeController()
    {
        WriteLog(this.GetType().Name, $"Initializing the core controller");

        LoadingSteps.Max = ConstantController.Loading_StartUp;
        LoadingSteps = (0, ConstantController.Loading_StartUp);

        LoadingStepCompleted();
    }

    public void LoadingStepCompleted()
    {
        WriteLog(this.GetType().Name, $"Marking loading step as complete");

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

        WriteLog(this.GetType().Name, $"Loading step has completed: {LoadingSteps.Progress} out of {LoadingSteps.Max}");
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

    public string GetSceneName()
	{
		WriteLog(this.GetType().Name, $"Getting the name of the current scene.");
		
		return SceneManager.GetActiveScene().name;
	}
#endregion
#region -------------------- Private Methods --------------------

#endregion
}}