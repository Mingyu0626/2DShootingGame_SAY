using UnityEngine;

public enum BulletType
{
    Main,
    Sub
}
public class Bullet : MonoBehaviour
{
    [Header("Basic")]
    [Tooltip("ÃÑ¾ËÀÇ ¹ß»ç ¼Óµµ")]
    [SerializeField] private float _speed;
    [Tooltip("ÃÑ¾ËÀÇ ¼Óµµ º¯·®")]
    [SerializeField] private float _speedVariance = 1f;
    [Tooltip("ÃÑ¾ËÀÇ °ø°Ý·Â")]
    [SerializeField] private int _damage;
    [Tooltip("ÃÑ¾ËÀÇ Áö¼Ó½Ã°£")]
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
    [Tooltip("ÃÑ¾ËÀÇ Å¸ÀÔ")]
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
    [Tooltip("ÃÑ¾Ë ±ËµµÀÇ ÁÖÆÄ¼ö")]
    [SerializeField] 
    private float _frequency;
    [Tooltip("ÃÑ¾Ë ±ËµµÀÇ ÁøÆø")]
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

    private void Awake()
    {
        Invoke(nameof(DestroyBullet), Duration);
    }

    private void Update()
    {
        Move();
        Snake();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy otherEnemy = other.GetComponent<Enemy>();
            if (otherEnemy != null)
            {
                Damage damage = new Damage
                {
                    Value = Damage,
                    Type = DamageType.Bullet,
                    From = gameObject
                };

                otherEnemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }


    }
    private void Move()
    {
        Vector2 direction = Vector2.up;
        transform.Translate(direction * Speed * Time.deltaTime);
    }
    private void Snake()
    {
        float offset = Mathf.Sin(Time.time * Frequency) * Amplitude;
        transform.position += transform.right * offset * Time.deltaTime;
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
