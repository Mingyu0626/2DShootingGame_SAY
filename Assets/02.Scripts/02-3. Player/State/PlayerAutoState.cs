using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerAutoState : IPlayerState, IMove, IFire
{
    private PlayerController _playerController;
    private PlayerData _playerData;
    private PlayerMoveUtils _playerMoveUtils;
    private PlayerFireUtils _playerFireUtils;
    private GameObject _target;

    private int UNDER_LINE = 4;
    private const float THRESHOLD = 0.01f;
    private const float DETACTION_RANGE = 6;
    public void Enter(PlayerController playerController)
    {
        _playerController = playerController;
        _playerController.CurrentPlayMode = PlayMode.Auto;
        _playerData = _playerController.GetComponent<PlayerData>();
        _playerMoveUtils = _playerController.GetComponent<PlayerMoveUtils>();
        _playerFireUtils = _playerController.GetComponent<PlayerFireUtils>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _playerController.PlayerStateContext.ChangeState(_playerController.ManualState);
        }

        FindNearestEnemy();
        Move();
        Fire();
    }

    public void Exit()
    {
        
    }
    public void Move()
    {
        if (_target == null) return;

        Vector2 direction = 
            _target.transform.position - _playerController.transform.position;
        float distance = direction.magnitude;

        if (Mathf.Abs(direction.x) <= THRESHOLD)
        {
            direction.x = 0;
        }

        // 멀면 앞으로 가까우면 뒤로
        if (distance > DETACTION_RANGE)
        {
            direction.y = 1;
        }
        else if (_playerController.transform.position.y < -UNDER_LINE)
        {
            direction.y = 0;
        }
        else
        {
            direction.y = -1;
        }
        direction.Normalize();
        _playerMoveUtils.PlayAnimation(direction);


        // 1. 새로운 위치 = 현재 위치 + 방향 * 속력 * 시간
        Vector3 newPosition = _playerController.transform.position 
            + (Vector3)(direction * _playerData.Speed) * Time.deltaTime;

        // 2. 위치 갱신
        _playerController.transform.position = newPosition;

    }
    private void FindNearestEnemy()
    {
        if (_target != null) return;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float targetDistance = Mathf.Infinity;
        foreach (var enemy in enemies)
        {
            targetDistance = Mathf.Min(targetDistance,
                Vector2.Distance(_playerController.transform.position, enemy.transform.position));
            _target = enemy;
        }
    }
    public void Fire()
    {
        if (!_playerData.IsCoolDown)
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
