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

namespace Atbt.Dialogue {
public enum RemarkTypeEnum {
#region -------------------- Enum List --------------------
    None = 0,
    Birthday,
    BirthdayGiftDisliked,
    BirthdayGiftHated,
    BirthdayGiftLiked,
    BirthdayGiftLoved,
    BirthdayGiftNeutral,
    Daylight,
    Festival,
    GiftDisliked,
    GiftHated,
    GiftLiked,
    GiftLoved,
    GiftNeutral,
    HeartLevel,
    ProposalAccept,
    ProposalDeny,
    QuestComplete,
    QuestHelp,
    QuestStart,
    Season,
    ShopGreet,
    ShopPurchase,
    Weather,
#endregion
}}