using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboHandler : MonoBehaviour
{

    public bool ComboLost { get; private set; }
    public bool CanChainCombo { get; private set; }         //Used to make the ChainAttack and the ComboDash/ComboJump
    public bool CanAirCombo { get; private set; }
    public bool CanFinisherMove { get; private set; }
    public bool SecondCombo { get; private set; }

    #region Cached Components
    public int ComboTypeInputA { get; private set; }
    public int ComboTypeInputB { get; private set; }

    [SerializeField]
    private PlayerData _playerdata;
    private Player _player;
    #endregion

    public float lastAttackTime = -100f;
    public float lastChainAttackTime = -100f;
    public int lastComboTypePressed = 0;
    public int comboTracker = 1;
    

    private void Start()
    {
        ComboTypeInputA = _playerdata.aInputComboType;
        ComboTypeInputB = _playerdata.bInputComboType;

        _player = GetComponent<Player>();
    }

    private void Update()
    {
        
    }

    #region Check Methods
    public void CheckEnemyHitbox()
    {
        _player.StateMachine.CurrentState.CheckEnemyHitbox();
    }

    public void CheckIfComboLost()
    {
        if (Time.time > lastAttackTime + _playerdata.comboLostTime)
        {
            comboTracker = 1;
            CannotChain();
            CannotPerformAirCombo();
            CannotSecondCombo();
            CannotFinisher();
        }
    }

    public void CheckIfChainLost()
    {
        if (Time.time > lastChainAttackTime + _playerdata.chainLostTime)
        {
            CannotChain();
        }
    }
    #endregion

    #region Can or Cannot Setters
    public void CanChain() => CanChainCombo = true;
    public void CannotChain() => CanChainCombo = false;
    public void CanFinisher() => CanFinisherMove = true;
    public void CannotFinisher() => CanFinisherMove = false;
    public void CanPerformAirCombo() => CanAirCombo = true;
    public void CannotPerformAirCombo() => CanAirCombo = false;
    public void CanSecondCombo()
    {
        SecondCombo = true;
        _player.Anim.SetBool("secondCombo", true);
    }
    public void CannotSecondCombo()
    {
        SecondCombo = false;
        _player.Anim.SetBool("secondCombo", false);
    } 
    #endregion

    public void ResetComboTracker() => comboTracker = 1;
    public void ResetComboAll()
    {
        CannotChain();
        CannotPerformAirCombo();
        CannotSecondCombo();
        CannotFinisher();
        ResetComboTracker();
    }

    public int GetAttackInputPressedType()
    {
        if (_player.InputHandler.AttackInputA)
        {
            return ComboTypeInputA;
        }
        else if (_player.InputHandler.AttackInputB)
        {
            return ComboTypeInputB;
        }
        else return 0;
    }


}
