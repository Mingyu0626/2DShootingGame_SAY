using System.Collections;
using UnityEngine;
public abstract class Bullet : MonoBehaviour, IProduct
{
    [Header("Basic")]
    [Tooltip("총알의 발사 속도")]
    [SerializeField] private float _speed;
    [Tooltip("총알의 속도 변량")]
    [SerializeField] private float _speedVariance = 1f;
    [Tooltip("총알의 공격력")]
    [SerializeField] private int _damage;
    [Tooltip("총알의 지속시간")]
    [SerializeField] private float _duration = 1f;
    public float Speed
    {
        get
        {
            return _speed;
        }
        private set
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
        get
        {
            return _speedVariance;
        }
        private set
        {
            _speedVariance = value;
        }
    }
    public float Duration
    {
        get { return _duration; }
        private set { }
    }
    public int Damage
    {
        get { return _damage; }
        private set { _damage = value; }
    }

    [Header("Type")]
    [Tooltip("총알의 타입")]
    [SerializeField] 
    BulletType _currentBulletType;
    public BulletType CurrentBulletType
    {
        get { return _currentBulletType; }
        private set
        {
            _currentBulletType = value;
        }
    }


    [Header("Snake")]
    [Tooltip("총알 궤도의 주파수")]
    [SerializeField] 
    private float _frequency;
    [Tooltip("총알 궤도의 진폭")]
    [SerializeField] 
    private float _amplitude;

    public float Frequency
    {
        get { return _frequency; }
        private set { _frequency = value;  }
    }
    public float Amplitude
    {
        get { return _amplitude; }
        private set { _amplitude = value; }
    }
    private void OnEnable()
    {
        StartCoroutine(BulletDuration());
    }
    private void FixedUpdate()
    {
        Move();
        Snake();
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    protected virtual void OnTriggerEnter2D(Collider2D other) {}

    public void Init()
    {
        
    }

    public abstract void Move();

    private void Snake()
    {
        float offset = Mathf.Sin(Time.time * Frequency) * Amplitude;
        transform.position += transform.right * offset * Time.deltaTime;
    }

    private IEnumerator BulletDuration()
    {

        yield return new WaitForSeconds(_duration);
        BulletPool.Instance.ReturnObject(this);
        gameObject.SetActive(false);
    }
}
