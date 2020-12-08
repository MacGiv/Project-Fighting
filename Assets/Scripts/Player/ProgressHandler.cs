using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressHandler : MonoBehaviour
{

    public int CurrentLevelNumber { get; private set; }
    public bool UniqueAttackEnabled { get; private set; }
    public bool ChainEnabled { get; private set; }
    public bool ChainMoveEnabled { get; private set; }
    public bool UpgradedUniqueAttackEnabled { get; private set; }
    public bool SecondComboEnabled { get; private set; }
    public bool FinisherEnabled { get; private set; }

    [SerializeField]
    private ProgressData progressData;

    private void Awake()
    {
        InitializeLevel(progressData.GetProgressLevel());
    }

    public void InitializeLevel (ProgressLevel currentLevel)
    {
        CurrentLevelNumber = currentLevel.leveLNumber;
        UniqueAttackEnabled = currentLevel.canUniqueAttack;
        ChainEnabled = currentLevel.canChainAttack;
        ChainMoveEnabled = currentLevel.canChainMove;
        UpgradedUniqueAttackEnabled = currentLevel.canUniqueSecond;
        SecondComboEnabled = currentLevel.canSecondCombo;
        FinisherEnabled = currentLevel.canFinisher;
    }

    public ProgressLevel UpdatePlayersLevel()
    {
        ProgressLevel newLevel = new ProgressLevel(CurrentLevelNumber, UniqueAttackEnabled, ChainEnabled, ChainMoveEnabled, SecondComboEnabled, FinisherEnabled);
        return newLevel;
    }



    #region Enablers

    public void EnableUniqueAttack() => UniqueAttackEnabled = true;
    public void EnableChain() => ChainEnabled = true;
    public void EnableUpgradedUniqueAttack() => UpgradedUniqueAttackEnabled = true;
    public void EnableSecondCombo() => SecondComboEnabled = true;
    public void EnableFinisher() => FinisherEnabled = true;
    #endregion
}
