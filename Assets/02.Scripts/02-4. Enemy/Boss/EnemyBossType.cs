using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossType: Enemy, IMove
{
    private int _currentFaze = 1;
    [Header("Bullet")]
    [Tooltip("���� �Ѿ� Prefab")]
    [SerializeField] private GameObject _bulletPrefab;

    [Header("CoolDown")]
    [Tooltip("����� ���� ���ð�(��Ÿ��)")]
    [SerializeField] private List<float> _coolTime;

    [Header("AttackFaze1")]
    [SerializeField] private Transform _muzzleTransformAttackFaze1;
    [SerializeField] private int _numOfBullet = 20;


    [Header("AttackFaze2")]
    [SerializeField] private List<Transform> _muzzleTransformAttackFaze2;
    private int _attackCount;

    [Header("AttackFaze3")]
    [SerializeField] private List<Transform> _muzzleTransformAttackFaze3;
    [Tooltip("�̵���")]
    [SerializeField] private float _moveArea = 3f;
    [Tooltip("���濡 �ɸ��� �̵��ð�")]
    [SerializeField] private float _moveDuration = 1f;


    protected override void Awake()
    {
        base.Awake();
        UI_Game.Instance.SetBossHPSliderEnable(true);
        UI_Game.Instance.InitBossHPSlider(EnemyData.MaxHealthPoint);
        Move();
        MoveMuzzleFaze3();
        StartCoroutine(Attack());
    }

    private void Update()
    {
        
    }
    private void OnDestroy()
    {
        BossManager.Instance.SetSpawnerEnable(true);
        UI_Game.Instance.SetBossHPSliderEnable(false);
    }
    public void Move()
    {
        transform.DOShakePosition(1f, 0.01f)
            .SetLoops(-1, LoopType.Yoyo);
    }
    public IEnumerator Attack()
    {
        while (true)
        {
            if (EnemyData.MaxHealthPoint * 0.7 <= EnemyData.CurrentHealthPoint)
            {
                AttackFaze1();
            }
            else if (EnemyData.MaxHealthPoint * 0.3 <= EnemyData.CurrentHealthPoint)
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
            currentMuzzleTransform.DOMove(targetPosition, _moveDuration)
                .SetLoops(-1, LoopType.Yoyo);
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
