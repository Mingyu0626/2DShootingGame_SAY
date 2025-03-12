using System.Collections;
using UnityEngine;



public class PlayerFire : MonoBehaviour
{
    [Header("Bullet")]
    [Tooltip("메인 총알 Prefab")]
    [SerializeField] private GameObject _mainBulletPrefab;
    [Tooltip("서브 총알 Prefab")]
    [SerializeField] private GameObject _subBulletPrefab;

    [Header("Muzzle")]
    [Tooltip("메인 총알이 발사되는 총구 Transform")]
    [SerializeField] private Transform[] _mainMuzzleTransformArray;
    [Tooltip("서브 총알이 발사되는 총구 Transform")]
    [SerializeField] private Transform[] _subMuzzleTransformArray;

    [Header("CoolDown")]
    [Tooltip("총알의 발사 대기시간(쿨타임)")]
    [SerializeField] private float _coolTime;
    private bool _isCoolDown = false;
    public float CoolTime
    {
        get { return _coolTime; }
        private set { _coolTime = value; }
    }
    public bool IsCoolDown
    {
        get { return _isCoolDown; }
        private set { _isCoolDown = value; }
    }

    /* 공격 모드 Enum*/
    /* 모드가 더 많아진다? 상태 패턴 써버리자. */
    public enum AttackMode
    {
        Manual, // 수동 공격
        Auto    // 자동 공격
    }
    private AttackMode _currentAttackMode;
    public AttackMode CurrentAttackMode
    {
        get { return _currentAttackMode; }
        set { _currentAttackMode = value; }
    }


    private void Awake()
    {
        CurrentAttackMode = AttackMode.Manual;
    }
    private void Update()
    {
        ChangeAttackMode();
        switch (CurrentAttackMode)
        {
            case AttackMode.Manual:
                {
                    FireManualMode();
                    break;
                }
            case AttackMode.Auto:
                {
                    FireAutoMode();
                    break;
                }
            default:
                {
                    FireManualMode();
                    break;
                }
        }
    }
    private void FireManualMode()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !IsCoolDown)
        {
            for (int i = 0; i < _mainMuzzleTransformArray.Length; i++)
            {
                Instantiate(_mainBulletPrefab, _mainMuzzleTransformArray[i].position,
                    _mainMuzzleTransformArray[i].rotation);
            }

            for (int i = 0; i < _subMuzzleTransformArray.Length; i++)
            {
                Instantiate(_subBulletPrefab, _subMuzzleTransformArray[i].position,
                    _subMuzzleTransformArray[i].rotation);
            }
            StartCoroutine(CoolDown());
        }
    }
    private void FireAutoMode()
    {
        if (!IsCoolDown)
        {
            for (int i = 0; i < _mainMuzzleTransformArray.Length; i++)
            {
                Instantiate(_mainBulletPrefab, _mainMuzzleTransformArray[i].position,
                    _mainMuzzleTransformArray[i].rotation);
            }

            for (int i = 0; i < _subMuzzleTransformArray.Length; i++)
            {
                Instantiate(_subBulletPrefab, _subMuzzleTransformArray[i].position,
                    _subMuzzleTransformArray[i].rotation);
            }
            StartCoroutine(CoolDown());
        }
    }
    private IEnumerator CoolDown()
    {
        IsCoolDown = true;
        yield return new WaitForSeconds(CoolTime);
        IsCoolDown = false;
    }
    private void ChangeAttackMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurrentAttackMode = AttackMode.Auto;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentAttackMode = AttackMode.Manual;
        }
    }
}
