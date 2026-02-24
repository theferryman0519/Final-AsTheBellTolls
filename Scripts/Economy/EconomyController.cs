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

namespace AsTheBellTolls.Economy {
public class EconomyController : Singleton<EconomyController> {

#region -------------------- Serialized Variables --------------------
    [Header("Sub Controllers")]
    [SerializeField] private CommissionsSubController _commissionsSubController;
    [SerializeField] private EconomySubController _economySubController;
    [SerializeField] private FestivalsSubController _festivalsSubController;
    [SerializeField] private MailSubController _mailSubController;
    [SerializeField] private QuestsSubController _questsSubController;
    [SerializeField] private ShopsSubController _shopsSubController;
    [SerializeField] private TownRankingSubController _townRankingSubController;
    [SerializeField] private TownWealthSubController _townWealthSubController;
#endregion
#region -------------------- Public Variables --------------------
    public CommissionsSubController Commissions => _commissionsSubController;
    public EconomySubController Economy => _economySubController;
    public FestivalsSubController Festivals => _festivalsSubController;
    public MailSubController Mail => _mailSubController;
    public QuestsSubController Quests => _questsSubController;
    public ShopsSubController Shops => _shopsSubController;
    public TownRankingSubController TownRanking => _townRankingSubController;
    public TownWealthSubController TownWealth => _townWealthSubController;
#endregion
#region -------------------- Private Variables --------------------
    
#endregion
#region -------------------- Initial Functions --------------------
    
#endregion
#region -------------------- Coroutines --------------------
    
#endregion
#region -------------------- Public Methods --------------------
    
#endregion
#region -------------------- Private Methods --------------------
    
#endregion
}}
