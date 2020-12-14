using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboHandler : MonoBehaviour
{

    public bool ComboLost { get; private set; }
    public bool CanUniqueAttack { get; private set; }
    public bool CanChainCombo { get; private set; }         //Used to make the ChainAttack and the ComboDash/ComboJump
    public bool CanChainMove { get; private set; }
    public bool CanAirCombo { get; private set; }
    public bool CanFinisherMove { get; private set; }
    public bool SecondCombo { get; private set; }


    #region Cached Components
    public int ComboTypeInputA { get; private set; }
    public int ComboTypeInputB { get; private set; }

    [SerializeField]
    private PlayerData _playerdata;
    private Player _player;
    private ProgressHandler _progressHandler;
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
        _progressHandler = FindObjectOfType<ProgressHandler>();
    }

    private void Update()
    {
        
    }

    #region Check Methods
    public void CheckEnemyHitbox() => _player.StateMachine.CurrentState.CheckEnemyHitbox();

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
            CannotChainMove();
        }
    }
    #endregion

    #region Can or Cannot Setters
    public void CanChain()
    {
        if (_progressHandler.ChainEnabled)
            CanChainCombo = true;
        else
            ResetComboTracker();
    }
    public void CanDoChainMove()
    {
        if (_progressHandler.ChainMoveEnabled)
        {
            CanChainMove = true;
        }
    }
    public void CanPerformAirCombo()
    {
        if (_progressHandler.SecondComboEnabled)
        {
            CanAirCombo = true;
        }
        else
        {
            ResetComboTracker();
            CannotPerformAirCombo();
        }
    }
    public void CanSecondCombo()
    {
        if (_progressHandler.SecondComboEnabled)
        {
            SecondCombo = true;
            _player.Anim.SetBool("secondCombo", true);
        }
        else
        {
            ResetComboTracker();
            CannotPerformAirCombo();
        }
    }
    public void CanFinisher()
    {
        if (_progressHandler.FinisherEnabled)
            CanFinisherMove = true;
        else
            ResetComboAll();
            

    }

    public void CannotChain() => CanChainCombo = false;
    public void CannotChainMove() => CanChainMove = false;
    public void CannotFinisher() => CanFinisherMove = false;
    public void CannotPerformAirCombo() => CanAirCombo = false;
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
