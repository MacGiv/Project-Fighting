using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressLevel 
{
    /* Level 1: 1, !CanUniqueAttack, !CanChain, !CanUniqueSecond, !CanSecondCombo, !CanFinisher
     * Level 2: 2, CanUniqueAttack, !CanChain, !CanUniqueSecond, !CanSecondCombo, !CanFinisher
     * Level 3: 3, CanUniqueAttack, CanChain, !CanUniqueSecond, !CanSecondCombo, !CanFinisher
     * Level 4: 4, CanUniqueAttack, CanChain, CanUniqueSecond, !CanSecondCombo, !CanFinisher
     * Level 5: 5, CanUniqueAttack, CanChain, CanUniqueSecond, CanSecondCombo, CanFinisher
    */
    public int LeveLNumber { get; private set; }
    public bool CanUniqueAttack { get; private set; }
    public bool CanChainAttack { get; private set; }
    public bool CanUniqueSecond { get; private set; }
    public bool CanSecondCombo { get; private set; }
    public bool CanFinisher { get; private set; }

    public ProgressLevel(int LeveLNumber, bool CanUniqueAttack, bool CanChain, bool CanSecondCombo, bool CanFinisher)
    {
        this.LeveLNumber = LeveLNumber;
        this.CanUniqueAttack = CanUniqueAttack;
        this.CanChainAttack = CanChain;
        this.CanSecondCombo = CanSecondCombo;
        this.CanFinisher = CanFinisher;
    }
}
