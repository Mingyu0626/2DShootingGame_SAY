using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum Direction
{
    Left,
    Right,
    Down
}


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
        _directionToPlayer = (_playerTransform.position - transform.position).normalized;
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
        transform.Translate(directionVector * Speed * Time.deltaTime);
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
        transform.Translate(_directionToPlayer * Speed * Time.deltaTime, Space.World);
    }
    private void TracePlayer()
    {
        Vector3 directionVector = (_playerTransform.position - transform.position).normalized;
        transform.Translate(directionVector * Speed * Time.deltaTime, Space.World);
    }
}
