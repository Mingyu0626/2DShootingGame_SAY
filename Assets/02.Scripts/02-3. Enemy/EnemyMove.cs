using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMove : MonoBehaviour
{
    [Header("Basic")]
    [Tooltip("적의 이동속도")]
    [SerializeField] 
    private float _speed;
    [Tooltip("이동 방향")]
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

    private Transform _playerTransform; // Target, Trace 타입
    private Vector3 _directionToPlayer; // Target 타입

    [Header("Snake")] // Shake 타입
    [Tooltip("총알 궤도의 주파수")]
    [SerializeField] private float _frequency;
    [Tooltip("총알 궤도의 진폭")]
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
        // Translate는, 회전에 민감하기 때문에 잘 사용되지 않는다.
        // Translate는, 조향이 필요할 때 쓰는게 좋다.
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
        // 기본값이 오른쪽을 바라보고 있기 때문에 90도를 더해준다.
        float angleZ = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0, 0, angleZ);
    }
}
