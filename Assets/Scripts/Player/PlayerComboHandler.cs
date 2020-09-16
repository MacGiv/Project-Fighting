using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboHandler : MonoBehaviour
{
    

    public int ComboTypeInputA { get; private set; }
    public int ComboTypeInputB { get; private set; }
    public bool ComboLost { get; private set; }
    public bool CanChainCombo { get; private set; }
    public bool CanAirCombo { get; private set; }
    public bool CanFinisherMove { get; private set; }

    #region Cached Components

    private Player _player;
    [SerializeField]
    private PlayerData _playerdata;
    #endregion

    public float lastAttackTime = -100f;
    public float lastChainAttackTime = -100f;
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
        }
    }

    public void CheckIfChainLost()
    {
        if (Time.time > lastChainAttackTime + _playerdata.chainLostTime)
        {
            CannotChain();
        }
    }

    public void ResetComboTracker() => comboTracker = 1;

    public void CanChain() => CanChainCombo = true;
    public void CannotChain() => CanChainCombo = false;
    public void CanFinisher() => CanFinisherMove = true;
    public void CannotFinisher() => CanFinisherMove = false;
    public void CanPerformAirCombo() => CanAirCombo = true;
    public void CannotPerformAirCombo() => CanAirCombo = false;

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
