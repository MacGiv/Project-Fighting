using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgressLevel 
{
    /* Level 1: 1, !CanUniqueAttack, !CanChain, !CanUniqueSecond, !CanSecondCombo, !CanFinisher
     * Level 2: 2, CanUniqueAttack, !CanChain, !CanUniqueSecond, !CanSecondCombo, !CanFinisher
     * Level 3: 3, CanUniqueAttack, CanChain, !CanUniqueSecond, !CanSecondCombo, !CanFinisher
     * Level 4: 4, CanUniqueAttack, CanChain, CanUniqueSecond, !CanSecondCombo, !CanFinisher
     * Level 5: 5, CanUniqueAttack, CanChain, CanUniqueSecond, CanSecondCombo, !CanFinisher
     * Level 5: 5, CanUniqueAttack, CanChain, CanUniqueSecond, CanSecondCombo, CanFinisher
    */
    public int leveLNumber;
    public bool canUniqueAttack;
    public bool canChainAttack;
    public bool canUniqueSecond;
    public bool canChainMove;
    public bool canSecondCombo;
    public bool canFinisher;

    public ProgressLevel(int LeveLNumber, bool CanUniqueAttack, bool CanChain, bool CanChainMove, bool CanSecondCombo, bool CanFinisher)
    {
        this.leveLNumber = LeveLNumber;
        this.canUniqueAttack = CanUniqueAttack;
        this.canChainAttack = CanChain;
        this.canChainMove = CanChainMove;
        this.canSecondCombo = CanSecondCombo;
        this.canFinisher = CanFinisher;
    }
}
