using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Scriptable Objects/BulletData")]
public class BulletData : ScriptableObject
{
    [Header("Basic")]
    [Tooltip("�Ѿ��� �߻� �ӵ�")]
    [SerializeField] private float _speed;
    [Tooltip("�Ѿ��� �ӵ� ����")]
    [SerializeField] private float _speedVariance = 1f;
    [Tooltip("�Ѿ��� ���ݷ�")]
    [SerializeField] private int _damage;
    [Tooltip("�Ѿ��� ���ӽð�")]
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
    [Tooltip("�Ѿ��� Ÿ��")]
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
    [Tooltip("�Ѿ� �˵��� ���ļ�")]
    [SerializeField]
    private float _frequency;
    [Tooltip("�Ѿ� �˵��� ����")]
    [SerializeField]
    private float _amplitude;

    public float Frequency
    {
        get { return _frequency; }
        private set { _frequency = value; }
    }
    public float Amplitude
    {
        get { return _amplitude; }
        private set { _amplitude = value; }
    }
}
