using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossType: Enemy, IMove
{
    private int _currentFaze = 1;
    [Header("Bullet")]
    [Tooltip("보스 총알 Prefab")]
    [SerializeField] private GameObject _bulletPrefab;

    [Header("CoolDown")]
    [Tooltip("페이즈별 공격 대기시간(쿨타임)")]
    [SerializeField] private List<float> _coolTime;
    private bool _isCoolDown = false;
    [SerializeField] private int _damage = 1;

    [Header("AttackFaze1")]
    [SerializeField] private Transform _muzzleTransformAttackFaze1;
    [SerializeField] private int _numOfBullet = 20;


    [Header("AttackFaze2")]
    [SerializeField] private List<Transform> _muzzleTransformAttackFaze2;

    [Header("AttackFaze3")]
    [SerializeField] private List<Transform> _muzzleTransformAttackFaze3;



    protected override void Awake()
    {
        base.Awake();
        UI_Game.Instance.SetBossHPSliderEnable(true);
        UI_Game.Instance.InitBossHPSlider(EnemyData.MaxHealthPoint);
        Move();
    }

    private void Update()
    {
        Attack();
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
    public void Attack()
    {
        if (!_isCoolDown)
        {
            if (EnemyData.MaxHealthPoint * 0.7 <= EnemyData.CurrentHealthPoint)
            {
                _currentFaze = 2;
                AttackTypeA();
            }
            else if (EnemyData.MaxHealthPoint * 0.3 <= EnemyData.CurrentHealthPoint)
            {
                _currentFaze = 3;
                StartCoroutine(AttackTypeB());
            }
            else
            {
                AttackTypeC();
            }

            StartCoroutine(CoolDown(_coolTime[_currentFaze - 1]));
        }
    }
    public void AttackTypeA()
    {
        float angle = 360 / _numOfBullet;
        for (int i = 0; i < _numOfBullet; i++)
        {
            Instantiate(_bulletPrefab,
                _muzzleTransformAttackFaze1.position,
                Quaternion.Euler(new Vector3(0, 0, angle * i)));
        }

    }
    public IEnumerator AttackTypeB()
    {
        int faze2MuzzleCount = _muzzleTransformAttackFaze2.Count;
        for (int i = 0; i < faze2MuzzleCount; i += 2)
        {
            Instantiate(_bulletPrefab, _muzzleTransformAttackFaze2[i]);
        }
        yield return new WaitForSeconds(_coolTime[1] / 2.0f);

        for (int i = 1; i < faze2MuzzleCount; i += 2)
        {
            Instantiate(_bulletPrefab, _muzzleTransformAttackFaze2[i]);
        }
    }
    public void AttackTypeC()
    {
        int faze3MuzzleCount = _muzzleTransformAttackFaze3.Count;
        for (int i = 0; i < faze3MuzzleCount; i++)
        {

        }
    }

    private IEnumerator CoolDown(float coolTime)
    {
        _isCoolDown = true;
        yield return new WaitForSeconds(coolTime);
        _isCoolDown = false;
    }
}
