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
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _playerController.PlayerStateContext.ChangeState(_playerController.InvincibleState);
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

#if UNITY_ANDROID
            direction = new Vector2
                (_playerController.DynamicJoystick.Horizontal,
                _playerController.DynamicJoystick.Vertical).normalized;
#endif
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
        // Input.GetKeyDown(KeyCode.LeftControl)
        if (!_playerData.IsCoolDown)
        {
            for (int i = 0; i < _playerData.MainMuzzleTransformArray.Length; i++)
            {
                BulletPool.Instance.GetObject(BulletType.Main,
                    _playerData.MainMuzzleTransformArray[i].position);

            }

            for (int i = 0; i < _playerData.SubMuzzleTransformArray.Length; i++)
            {
                BulletPool.Instance.GetObject(BulletType.Sub,
                    _playerData.SubMuzzleTransformArray[i].position);
            }
            _playerController.StartStateCoroutine(_playerFireUtils.CoolDown());
        }
    }
}
