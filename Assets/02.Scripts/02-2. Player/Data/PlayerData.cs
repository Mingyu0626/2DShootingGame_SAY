using UnityEngine;

public class PlayerData : MonoBehaviour
{
    //[Header("HP")]
    //[SerializeField]
    //private int _maxHealthPoint = 3;
    //[SerializeField]
    //private int _currentHealthPoint;
    //public int MaxHealthPoint
    //{
    //    get { return _maxHealthPoint; }
    //    set { _maxHealthPoint = value; }
    //}
    //public int CurrentHealthPoint
    //{
    //    get { return _currentHealthPoint; }
    //    set
    //    {
    //        _currentHealthPoint = Mathf.Clamp(value, 0, _maxHealthPoint);
    //        if (_currentHealthPoint == 0)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}

    // 라벨링은 날 잡아서 하죠
    [Header("Move")]
    [SerializeField]
    private float _speed = 3f;
    private float _speedVariance = 1f;
    private float _horizontalLimit = 4.5f;
    private float _verticalLimit = 3.0f;
    public float Speed
    {
        get { return _speed; }
        set
        {
            if (0 <= value)
            {
                _speed = value;
                Debug.Log(_speed);
            }
        }
    }
    public float SpeedVariance
    {
        get { return _speedVariance; }
        private set { _speedVariance = value; }
    }
    public float HorizontalLimit
    {
        get { return _horizontalLimit; }
        private set { _horizontalLimit = value; }
    }
    public float VerticalLimit
    {
        get { return _verticalLimit; }
        private set { _verticalLimit = value; }
    }


    [Header("Fire")]
    [Header("Bullet")]
    [Tooltip("메인 총알 Prefab")]
    [SerializeField] private GameObject _mainBulletPrefab;
    public GameObject MainBulletPrefab { get => _mainBulletPrefab; }
    [Tooltip("서브 총알 Prefab")]
    [SerializeField] private GameObject _subBulletPrefab;
    public GameObject SubBulletPrefab { get => _subBulletPrefab; }

    [Header("Muzzle")]
    [Tooltip("메인 총알이 발사되는 총구 Transform")]
    [SerializeField] private Transform[] _mainMuzzleTransformArray;
    public Transform[] MainMuzzleTransformArray { get => _mainMuzzleTransformArray; }
    [Tooltip("서브 총알이 발사되는 총구 Transform")]
    [SerializeField] private Transform[] _subMuzzleTransformArray;
    public Transform[] SubMuzzleTransformArray { get => _subMuzzleTransformArray; }

    [Header("CoolDown")]
    [Tooltip("총알의 발사 대기시간(쿨타임)")]
    [SerializeField] private float _coolTime;
    private bool _isCoolDown = false;
    public float CoolTime
    {
        get { return _coolTime; }
        set { _coolTime = value; }
    }
    public bool IsCoolDown
    {
        get { return _isCoolDown; }
        set { _isCoolDown = value; }
    }
}
