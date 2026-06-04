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
using Atbt.Enum;

namespace Atbt.Controller {
public class ConstantController : Singleton<ConstantController> {

#region -------------------- Serialized Variables --------------------

#endregion
#region -------------------- Public Variables --------------------
    // Loading Sets
    public const int Loading_StartUp = 21;

    // Titles
    public const string Game_Title = "As The Bell Tolls";
    public const string Game_Studio = "Ferryman Studio";
    public const string Game_Email = "ferrymanstudios@gmail.com";

    // Screen Dimensions
    public float Screen_Height;
    public float Screen_Width;

    // Multipliers
    public const float Multiplier_Fading = 0.5f;

    // Thresholds
    public const float Threshold_MoveInputDeadzone = 0.5f;

    // Dictionaries
    public Dictionary<QualityTypeEnum, float> QualityMultipliers;
    public Dictionary<RelationshipPointsTypeEnum, int> RelationshipPointAdditions;
    public Dictionary<int, int> HeartThresholds;
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
        CoreController.Inst.WriteLog(this.GetType().Name, $"Initializing the constant controller");

        UpdateQualityMultipliersDict();
        UpdateRelationshipPointAdditionsDict();
        UpdateHeartThresholdsDict();

        CoreController.Inst.LoadingStepCompleted();
    }
#endregion
#region -------------------- Private Methods --------------------
    private void UpdateQualityMultipliersDict()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the QualityMultipliers dictionary");

        QualityMultipliers.Clear();

        QualityMultipliers.Add(QualityTypeEnum.Base, 1f);
        QualityMultipliers.Add(QualityTypeEnum.Copper, 1.1f);
        QualityMultipliers.Add(QualityTypeEnum.Silver, 1.25f);
        QualityMultipliers.Add(QualityTypeEnum.Gold, 1.5f);
        QualityMultipliers.Add(QualityTypeEnum.Cobalt, 1.9f);
    }

    private void UpdateRelationshipPointAdditionsDict()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the RelationshipPointAdditions dictionary");

        RelationshipPointAdditions.Clear();

        RelationshipPointAdditions.Add(RelationshipPointsTypeEnum.AuroraWatch, 100);
        RelationshipPointAdditions.Add(RelationshipPointsTypeEnum.BirthdayGiftDisliked, -80);
        RelationshipPointAdditions.Add(RelationshipPointsTypeEnum.BirthdayGiftHated, -160);
        RelationshipPointAdditions.Add(RelationshipPointsTypeEnum.BirthdayGiftLiked, 80);
        RelationshipPointAdditions.Add(RelationshipPointsTypeEnum.BirthdayGiftLoved, 160);
        RelationshipPointAdditions.Add(RelationshipPointsTypeEnum.BirthdayGiftNeutral, 10);
        RelationshipPointAdditions.Add(RelationshipPointsTypeEnum.FirstTalk, 5);
        RelationshipPointAdditions.Add(RelationshipPointsTypeEnum.GiftDisliked, -20);
        RelationshipPointAdditions.Add(RelationshipPointsTypeEnum.GiftHated, -40);
        RelationshipPointAdditions.Add(RelationshipPointsTypeEnum.GiftLiked, 20);
        RelationshipPointAdditions.Add(RelationshipPointsTypeEnum.GiftLoved, 40);
        RelationshipPointAdditions.Add(RelationshipPointsTypeEnum.GiftNeutral, 0);
        RelationshipPointAdditions.Add(RelationshipPointsTypeEnum.SharedMeal, 30);
        RelationshipPointAdditions.Add(RelationshipPointsTypeEnum.QuestComplete, 75);
    }

    private void UpdateHeartThresholdsDict()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Updating the HeartThresholds dictionary");

        HeartThresholds.Clear();

        HeartThresholds.Add(0, 0);
        HeartThresholds.Add(1, 100);
        HeartThresholds.Add(2, 225);
        HeartThresholds.Add(3, 375);
        HeartThresholds.Add(4, 550);
        HeartThresholds.Add(5, 750);
        HeartThresholds.Add(6, 975);
        HeartThresholds.Add(7, 1225);
        HeartThresholds.Add(8, 1500);
        HeartThresholds.Add(9, 1800);
        HeartThresholds.Add(10, 2100);
        HeartThresholds.Add(11, 2500);
        HeartThresholds.Add(12, 3000);
        HeartThresholds.Add(13, 3600);
        HeartThresholds.Add(14, 4300);
        HeartThresholds.Add(15, 5100);
    }
#endregion
}}
