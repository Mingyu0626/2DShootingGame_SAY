using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManualState : IPlayerState, IMove, IFire
{
    private PlayerController _playerController;
    private PlayerData _playerData;
    private PlayerMoveUtils _playerMoveUtils;
    private PlayerFireUtils _playerFireUtils;
    public void Enter(PlayerController playerController)
    {
        _playerController = playerController;
        _playerController.CurrentPlayMode = PlayMode.Manual;
        _playerData = _playerController.GetComponent<PlayerData>();
        _playerMoveUtils = _playerController.GetComponent<PlayerMoveUtils>();
        _playerFireUtils = _playerController.GetComponent<PlayerFireUtils>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _playerController.PlayerStateContext.ChangeState(_playerController.AutoState);
        }

        Move();
        Fire();
        _playerMoveUtils.SpeedCheck();
    }

    public void Exit()
    {

    }
    public void Move()
    {
        if (_playerMoveUtils.IsPlayerInView())
        {
            Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")).normalized;
            _playerController.transform.Translate(direction * _playerData.Speed * Time.deltaTime);
            _playerMoveUtils.ClampPlayerHorizontalPosition();
            _playerMoveUtils.PlayAnimation(direction);
        }
        else
        {
            _playerMoveUtils.ReversePlayerVerticalPosition();
        }
    }
    public void Fire()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !_playerData.IsCoolDown)
        {
            for (int i = 0; i < _playerData.MainMuzzleTransformArray.Length; i++)
            {
                _playerController.InstantiateBullet
                    (_playerData.MainBulletPrefab, 
                    _playerData.MainMuzzleTransformArray[i].position,
                    _playerData.MainMuzzleTransformArray[i].rotation);
            }

            for (int i = 0; i < _playerData.SubMuzzleTransformArray.Length; i++)
            {
                _playerController.InstantiateBullet
                    (_playerData.SubBulletPrefab,
                    _playerData.SubMuzzleTransformArray[i].position,
                    _playerData.SubMuzzleTransformArray[i].rotation);
            }
            _playerController.StartStateCoroutine(_playerFireUtils.CoolDown());
        }
    }
}
