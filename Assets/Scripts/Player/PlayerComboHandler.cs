using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboHandler : MonoBehaviour
{
    #region Cached Components

    public int comboType;

    private PlayerGroundedAttackState _groundedAttackState;
    private Player _player;
    [SerializeField]
    private PlayerData _playerdata;
    #endregion

    public float lastAttackTime = -100f;
    public int comboTracker = 1;

    private void Start()
    {
        _player = GetComponent<Player>();
        _groundedAttackState = _player.GroundedAttackState;
    }

    private void Update()
    {
        
    }


    public void CheckEnemyHitbox()
    {
        _groundedAttackState.CheckEnemyHitbox();
    }

    public void CheckIfComboLost()
    {
        if (Time.time > lastAttackTime + _playerdata.comboLostTime)
        {
            comboTracker = 1;
        }
    }

}
