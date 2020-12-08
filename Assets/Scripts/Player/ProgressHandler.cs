using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressHandler : MonoBehaviour
{
    public bool ChainEnabled { get; private set; }
    public bool SecondComboEnabled { get; private set; }
    public bool UniqueAttackEnabled { get; private set; }
    public bool UpgradedUniqueAttackEnabled { get; private set; }
    public bool FinisherEnabled { get; private set; }




    #region Enablers

    public void EnableChain() => ChainEnabled = true;
    public void EnableSecondCombo() => SecondComboEnabled = true;
    public void EnableUniqueAttack() => UniqueAttackEnabled = true;
    public void EnableUpgradedUniqueAttack() => UpgradedUniqueAttackEnabled = true;
    public void EnableFinisher() => FinisherEnabled = true;
    #endregion
}
