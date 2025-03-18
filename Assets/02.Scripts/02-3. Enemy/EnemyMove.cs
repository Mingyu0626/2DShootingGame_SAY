using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMove : MonoBehaviour
{
    [Header("Basic")]
    [Tooltip("���� �̵��ӵ�")]
    [SerializeField] 
    private float _speed;
    [Tooltip("�̵� ����")]
    [SerializeField]
    private Direction _directionEnum;

    public float Speed
    {
        get { return _speed; }
        private set { _speed = value; }
    }
    public Direction DirectionEnum
    {
        get { return _directionEnum; }
        set { _directionEnum = value; }
    }

    private Enemy _enemy;
    private Dictionary<Direction, Vector2> _directionDictionary = new Dictionary<Direction, Vector2>()
    {
        { Direction.Down, Vector2.down },
        { Direction.Left, Vector2.left},
        { Direction.Right, Vector2.right }
    };

    private Transform _playerTransform; // Target, Trace Ÿ��
    private Vector3 _directionToPlayer; // Target Ÿ��

    [Header("Snake")] // Shake Ÿ��
    [Tooltip("�Ѿ� �˵��� ���ļ�")]
    [SerializeField] private float _frequency;
    [Tooltip("�Ѿ� �˵��� ����")]
    [SerializeField] private float _amplitude;
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


    private void Awake()
    {
        _enemy = gameObject.GetComponent<Enemy>();
    }
    private void Start()
    {
        _playerTransform = Player.Instance.transform;
        _directionToPlayer = _playerTransform.position - transform.position;

        if (_enemy.CurrentEnemyType == EnemyType.Target)
        {
            LookAtPlayer(_directionToPlayer);
        }
    }
    private void Update()
    {
        switch (_enemy.CurrentEnemyType)
        {
            case EnemyType.Normal:
                {
                    Move(_directionEnum);
                    break;
                }
            case EnemyType.Split:
                {
                    Move(_directionEnum);
                    break;
                }
            case EnemyType.Shake:
                {
                    Move(_directionEnum);
                    SinCurve();
                    break;
                }
            case EnemyType.Trace:
                {
                    TracePlayer();
                    break;
                }
            case EnemyType.Target:
                {
                    TargetPlayer();
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
    private void Move(Direction dir)
    {
        Vector2 directionVector = _directionDictionary[dir];
        // transform.Translate(directionVector * Speed * Time.deltaTime);
        transform.position += (Vector3)(directionVector * Speed * Time.deltaTime);
    }
    private void Move()
    {
        Vector2 directionVector = Vector2.down;
        transform.Translate(directionVector * Speed * Time.deltaTime);

    }
    private void SinCurve()
    {
        float offset = Mathf.Sin(Time.time * Frequency) * Amplitude;
        transform.position += transform.right * offset * Time.deltaTime;
    }
    private void TargetPlayer()
    {
        // Translate��, ȸ���� �ΰ��ϱ� ������ �� ������ �ʴ´�.
        // Translate��, ������ �ʿ��� �� ���°� ����.
        // transform.Translate(_directionToPlayer.normalized * Speed * Time.deltaTime, Space.World);
        transform.position += (Vector3)(_directionToPlayer.normalized * Speed * Time.deltaTime);
    }
    private void TracePlayer()
    {
        Vector3 directionVector = (_playerTransform.position - transform.position).normalized;
        transform.position += (Vector3)(directionVector * Speed * Time.deltaTime);
        LookAtPlayer(_playerTransform.position - transform.position);
    }

    private void LookAtPlayer(Vector2 distance)
    {
        // �⺻���� �������� �ٶ󺸰� �ֱ� ������ 90���� �����ش�.
        float angleZ = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0, 0, angleZ);
    }
}
