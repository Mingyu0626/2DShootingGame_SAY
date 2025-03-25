using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossType: Enemy, IEnemyMove
{
    private int _currentFaze = 1;
    [Header("Bullet")]
    [Tooltip("보스 총알 Prefab")]
    [SerializeField] private GameObject _bulletPrefab;

    [Header("CoolDown")]
    [Tooltip("페이즈별 공격 대기시간(쿨타임)")]
    [SerializeField] private List<float> _coolTime;

    [Header("AttackFaze1")]
    [SerializeField] private Transform _muzzleTransformAttackFaze1;
    [SerializeField] private int _numOfBullet = 20;


    [Header("AttackFaze2")]
    [SerializeField] private List<Transform> _muzzleTransformAttackFaze2;
    private int _attackCount;

    [Header("AttackFaze3")]
    [SerializeField] private List<Transform> _muzzleTransformAttackFaze3;
    private List<Tween> _muzzleShakeTweens = new List<Tween>();
    [Tooltip("이동량")]
    [SerializeField] private float _moveArea = 3f;
    [Tooltip("변경에 걸리는 이동시간")]
    [SerializeField] private float _moveDuration = 1f;

    private Tween _shakeTweenBoss;


    protected override void Awake()
    {
        base.Awake();
    }
    private void OnEnable()
    {
        if (BossManager.Instance.IsBossSpawned)
        {
            UI_Game.Instance.SetBossHPSliderEnable(true);
            UI_Game.Instance.InitBossHPSlider(EnemyData.MaxHealthPoint);
            UI_Game.Instance.RefreshBossUI(CurrentHealthPoint);
            Move();
            MoveMuzzleFaze3();
            StartCoroutine(Attack());
        }
    }
    private void OnDisable()
    {
        BossManager.Instance.SetSpawnerEnable(true);
        UI_Game.Instance.SetBossHPSliderEnable(false);
        _shakeTweenBoss.Kill();
        for (int i = 0; i < _muzzleShakeTweens.Count; i++)
        {
            _muzzleShakeTweens[i].Kill();
        }
        _muzzleShakeTweens.Clear();
        StopAllCoroutines();
        
    }

    public void Move()
    {
        _shakeTweenBoss = transform.DOShakePosition(1f, 0.01f)
            .SetLoops(-1, LoopType.Yoyo);
    }
    public void Move(Direction dir = Direction.Down)
    {
        throw new System.NotImplementedException();
    }
    public IEnumerator Attack()
    {
        while (true)
        {
            if (EnemyData.MaxHealthPoint * 0.7 <= CurrentHealthPoint)
            {
                AttackFaze1();
            }
            else if (EnemyData.MaxHealthPoint * 0.3 <= CurrentHealthPoint)
            {
                _currentFaze = 2;
                AttackFaze2();
            }
            else
            {
                _currentFaze = 3;
                AttackFaze3();
            }
            yield return new WaitForSeconds(_coolTime[_currentFaze - 1]);
        }
    }
    public void AttackFaze1()
    {
        float angleStep = 360f / _numOfBullet;
        for (int i = 0; i < _numOfBullet; i++)
        {
            float angle = angleStep * i;

            Instantiate(_bulletPrefab, _muzzleTransformAttackFaze1.position,
                Quaternion.Euler(0, 0, angle));
        }
    }
    public void AttackFaze2()
    {
        int idx = _attackCount % 2;
        for (int i = idx; i < _muzzleTransformAttackFaze2.Count; i += 2)
        {
            Instantiate(_bulletPrefab, _muzzleTransformAttackFaze2[i]);
        }
        _attackCount++;
    }

    public void MoveMuzzleFaze3()
    {
        float dir = -1f;
        Transform currentMuzzleTransform;
        for (int i = 0; i < _muzzleTransformAttackFaze3.Count; i++)
        {
            currentMuzzleTransform = _muzzleTransformAttackFaze3[i].transform;
            Vector3 targetPosition = new Vector3
                (_moveArea * dir, 
                currentMuzzleTransform.position.y,
                currentMuzzleTransform.position.z);

            Tween curMuzzleShakeTween = currentMuzzleTransform.DOMove(targetPosition, _moveDuration)
                .SetLoops(-1, LoopType.Yoyo);
            _muzzleShakeTweens.Add(curMuzzleShakeTween);
            dir *= -1f;
        }

    }

    public void AttackFaze3()
    {
        for (int i = 0; i < _muzzleTransformAttackFaze3.Count; i++)
        {
            Instantiate(_bulletPrefab, _muzzleTransformAttackFaze3[i].transform.position, Quaternion.identity);
        }
    }
}
