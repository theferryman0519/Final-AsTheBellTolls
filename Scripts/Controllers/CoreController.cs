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
    
#endregion
#region -------------------- Private Variables --------------------
    private (int progress, int max) _loadingSteps;

    private Dictionary<string, string> _sceneNames = new();
#endregion
#region -------------------- Initial Functions --------------------
    
#endregion
#region -------------------- Coroutines --------------------
    void Start()
    {
        SceneManager.LoadSceneAsync(GetSceneName("Scene_Main_00"), LoadSceneMode.Single);

        InitializeController();
    }
#endregion
#region -------------------- Public Methods --------------------
    public void InitializeController()
    {
        WriteLog(this.GetType().Name, $"Intializing the core controller.");

        SetScenes();
        ControllerLoaded();
    }
    
    public void ControllerLoaded()
    {
        WriteLog(this.GetType().Name, $"Loading step has completed.");
		
        int newProgress = Mathf.Clamp(_loadingSteps.progress + 1, 0, _loadingSteps.max);
        _loadingSteps = (newProgress, _loadingSteps.max);
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
        
        if (_sceneNames.TryGetValue(sceneName, out string scene))
        {
            SceneManager.LoadScene(scene);
        }

        else
        {
            WriteError(this.GetType().Name, $"Cannot load requested scene of {sceneName}.");
        }
    }

    public string GetSceneName(string sceneName)
    {
        WriteLog(this.GetType().Name, $"Getting the name of the current scene.");
		
        if (_sceneNames.TryGetValue(sceneName, out string scene))
        {
            return scene;
        }

        else
        {
            WriteError(this.GetType().Name, $"Cannot load requested scene of {sceneName}.");
            
            return string.Empty;
        }
    }
    
    public string GetCurrentSceneName()
    {
        WriteLog(this.GetType().Name, $"Getting the name of the current scene.");
		
        return SceneManager.GetActiveScene().name;
    }
#endregion
#region -------------------- Private Methods --------------------
    private void SetScenes()
    {
        WriteLog(this.GetType().Name, $"Setting all scene names.");

        _sceneNames.Clear();
        
        // Persistent Scenes
        _sceneNames.Add("Scene_Persistent_00", "Persistent00_Main");
        
        // Main Scenes
        _sceneNames.Add("Scene_Main_00", "Main00_Studio");
        _sceneNames.Add("Scene_Main_01", "Main01_Title");
        _sceneNames.Add("Scene_Main_02", "Main02_Menu");
        _sceneNames.Add("Scene_Main_03", "Main03_LoadGames");
        _sceneNames.Add("Scene_Main_04", "Main04_Credits");
        
        // Intro Scenes
        _sceneNames.Add("Scene_Intro_00", "Intro00_Carriage");
        _sceneNames.Add("Scene_Intro_01", "Intro01_AvatarCreation");
        _sceneNames.Add("Scene_Intro_02", "Intro02_WindingBanksInn");
        _sceneNames.Add("Scene_Intro_03", "Intro03_Blackmere");
        _sceneNames.Add("Scene_Intro_04", "Intro04_PendrelleManorGrounds");
        
        // Exterior Scenes
        _sceneNames.Add("Scene_Exterior_00", "Exterior00_Blackmere");
        _sceneNames.Add("Scene_Exterior_01", "Exterior01_GloamwoodForest");
        _sceneNames.Add("Scene_Exterior_02", "Exterior02_GloamwoodDepths");
        _sceneNames.Add("Scene_Exterior_03", "Exterior03_IronveilPeak");
        _sceneNames.Add("Scene_Exterior_04", "Exterior04_SteelridgeWatch");
        _sceneNames.Add("Scene_Exterior_05", "Exterior05_GraythorneLake");
        _sceneNames.Add("Scene_Exterior_06", "Exterior06_AshfallMines");
        _sceneNames.Add("Scene_Exterior_07", "Exterior07_MemorialHillGrounds");
        _sceneNames.Add("Scene_Exterior_08", "Exterior08_MercerFarmsGrounds");
        _sceneNames.Add("Scene_Exterior_09", "Exterior09_PendrelleManorGrounds");
        
        // Interior Scenes
        _sceneNames.Add("Scene_Interior_00", "Interior00_PendrelleManor");
        _sceneNames.Add("Scene_Interior_01", "Interior01_MercerFarms");
        _sceneNames.Add("Scene_Interior_02", "Interior02_MemorialHill");
        _sceneNames.Add("Scene_Interior_03", "Interior03_1ResidentLane");
        _sceneNames.Add("Scene_Interior_04", "Interior04_2ResidentLane");
        _sceneNames.Add("Scene_Interior_05", "Interior05_3ResidentLane");
        _sceneNames.Add("Scene_Interior_06", "Interior06_4ResidentLane");
        _sceneNames.Add("Scene_Interior_07", "Interior07_5ResidentLane");
        _sceneNames.Add("Scene_Interior_08", "Interior08_6ResidentLane");
        _sceneNames.Add("Scene_Interior_09", "Interior09_PublicLibrary");
        _sceneNames.Add("Scene_Interior_10", "Interior10_PublicSchool");
        _sceneNames.Add("Scene_Interior_11", "Interior11_HallOfWonder");
        _sceneNames.Add("Scene_Interior_12", "Interior12_RiverbendFishery");
        _sceneNames.Add("Scene_Interior_13", "Interior13_WindingBanksInn");
        _sceneNames.Add("Scene_Interior_14", "Interior14_BankAndExchange");
        _sceneNames.Add("Scene_Interior_15", "Interior15_GeneralStore");
        _sceneNames.Add("Scene_Interior_16", "Interior16_TownHall");
        _sceneNames.Add("Scene_Interior_17", "Interior17_IronveilForge");
        _sceneNames.Add("Scene_Interior_18", "Interior18_KleinWoodworks");
        _sceneNames.Add("Scene_Interior_19", "Interior19_RainAndHale");
        _sceneNames.Add("Scene_Interior_20", "Interior20_ThreadAndThimble");
        _sceneNames.Add("Scene_Interior_21", "Interior21_WeissDesignStudio");
        
        // Festival Scenes
        _sceneNames.Add("Scene_Festival_00", "Festival00_RiverRemembranceDay");
        _sceneNames.Add("Scene_Festival_01", "Festival01_SeedwakeBrunch");
        _sceneNames.Add("Scene_Festival_02", "Festival02_BlackmereTradeFair");
        _sceneNames.Add("Scene_Festival_03", "Festival03_FunInTheSunFestival");
        _sceneNames.Add("Scene_Festival_04", "Festival04_GrandShowcase");
        _sceneNames.Add("Scene_Festival_05", "Festival05_HarvestSupper");
        _sceneNames.Add("Scene_Festival_06", "Festival06_HollowMoonNight");
        _sceneNames.Add("Scene_Festival_07", "Festival07_SnowBellsEve");
        _sceneNames.Add("Scene_Festival_08", "Festival08_TollOfHearths");
        _sceneNames.Add("Scene_Festival_09", "Festival09_TroutTrials");
        _sceneNames.Add("Scene_Festival_10", "Festival10_CookingWithTheTwins");
        _sceneNames.Add("Scene_Festival_11", "Festival11_SalmonRun");
        _sceneNames.Add("Scene_Festival_12", "Festival12_AuroraWatch");
        _sceneNames.Add("Scene_Festival_13", "Festival13_BreakfastAtTheInn");
        _sceneNames.Add("Scene_Festival_14", "Festival14_SupperWithTheMercers");
        
        // Night Scenes
        _sceneNames.Add("Scene_Night_00", "Night00_Summary");
        _sceneNames.Add("Scene_Night_01", "Night01_Upcoming");
        _sceneNames.Add("Scene_Night_02", "Night02_SaveGame");
        _sceneNames.Add("Scene_Night_03", "Night03_NextDay");
    }
#endregion
}}