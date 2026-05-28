// Main Dependencies
using DG.Tweening;
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
public class UiController : Singleton<UiController> {

#region -------------------- Serialized Variables --------------------
    [Header("UI Elements")]
    [SerializeField] private UiElementHud Hud;
    [SerializeField] private UiElementPause Pause;
    [SerializeField] private UiElementDialogue Dialogue;
    [SerializeField] private UiElementInventory Inventory;
    [SerializeField] private UiElementItemTransfer ItemTransfer;
    [SerializeField] private UiElementQuests Quests;
    [SerializeField] private UiElementInventions Inventions;
    [SerializeField] private UiElementNightSummary NightSummary;
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
    // Controls menu panels
    // Controls HUD
    // Controls notification popups
    // Controls end of day summaries

    public void InitializeController()
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Initializing the UI controller");

        CoreController.Inst.LoadingStepCompleted();
    }

    public void ShowHideHud(bool isShowing)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Showing/hiding the HUD");

        FadeCanvas(Hud.MainCanvas, isShowing);
    }

    public void ShowHidePauseMenu(bool isShowing)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Showing/hiding the pause menu");

        FadeCanvas(Pause.MainCanvas, isShowing);
    }

    public void ShowHideDialogueMenu(bool isShowing)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Showing/hiding the dialogue menu");

        FadeCanvas(Dialogue.MainCanvas, isShowing);
    }

    public void ShowHideInventoryMenu(bool isShowing)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Showing/hiding the inventory menu");

        FadeCanvas(Inventory.MainCanvas, isShowing);
    }

    public void ShowHideItemTransferMenu(bool isShowing)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Showing/hiding the item transfer menu");

        FadeCanvas(ItemTransfer.MainCanvas, isShowing);
    }

    public void ShowHideQuestsMenu(bool isShowing)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Showing/hiding the quests menu");

        FadeCanvas(Quests.MainCanvas, isShowing);
    }

    public void ShowHideInventionsMenu(bool isShowing)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Showing/hiding the inventions tree menu");

        FadeCanvas(Inventions.MainCanvas, isShowing);
    }

    public void ShowHideNightSummaryMenu(bool isShowing)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Showing/hiding the end-of-day summary menu");

        FadeCanvas(NightSummary.MainCanvas, isShowing);
    }
#endregion
#region -------------------- Private Methods --------------------
    private void FadeCanvas(Canvas canvas, bool isFadingIn, Action continueAction = null)
    {
        CoreController.Inst.WriteLog(this.GetType().Name, $"Fading canvas element");

        Sequence sequence = DOTween.Sequence();

        float initialAlpha = isFadingIn ? 0f : 1f;
        float finalAlpha = isFadingIn ? 1f : 0f;

        canvas.alpha = initialAlpha;
        sequence.Join(canvas.DOFade(finalAlpha, ConstantController.Multiplier_Fading));

        sequence.OnComplete(() =>
        {
            continueAction?.Invoke();
        });

        sequence.Play();
    }
#endregion
}}
