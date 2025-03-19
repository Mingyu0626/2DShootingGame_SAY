using UnityEngine;

public class EnemyTargetType : MonoBehaviour, IEnemy, IEnemyMove
{
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private EnemyData _enemyData;

    private Transform _playerTransform;
    private Vector3 _directionToPlayer;
    public EnemyType EnemyType
    {
        get { return _enemyType; }
        set { _enemyType = value; }
    }

    public void Init()
    {
        EnemyType = _enemyType;
        _enemyData.DirectionEnum = Direction.Down;
        LookAtPlayer(_directionToPlayer);
    }
    public void Init(Direction dir)
    {
        EnemyType = _enemyType;
        _enemyData.DirectionEnum = dir;
        LookAtPlayer(_directionToPlayer);
    }
    private void Update()
    {
        Move();
    }
    public void Move()
    {
        // Translate는, 회전에 민감하기 때문에 잘 사용되지 않는다.
        // Translate는, 조향이 필요할 때 쓰는게 좋다.
        // transform.Translate(_directionToPlayer.normalized * Speed * Time.deltaTime, Space.World);
        transform.position += (Vector3)(_directionToPlayer.normalized * _enemyData.Speed * Time.deltaTime);
    }
    private void LookAtPlayer(Vector2 distance)
    {
        // 기본값이 오른쪽을 바라보고 있기 때문에 90도를 더해준다.
        float angleZ = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0, 0, angleZ);
    }
}
