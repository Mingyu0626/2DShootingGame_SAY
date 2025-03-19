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
        // Translate��, ȸ���� �ΰ��ϱ� ������ �� ������ �ʴ´�.
        // Translate��, ������ �ʿ��� �� ���°� ����.
        // transform.Translate(_directionToPlayer.normalized * Speed * Time.deltaTime, Space.World);
        transform.position += (Vector3)(_directionToPlayer.normalized * _enemyData.Speed * Time.deltaTime);
    }
    private void LookAtPlayer(Vector2 distance)
    {
        // �⺻���� �������� �ٶ󺸰� �ֱ� ������ 90���� �����ش�.
        float angleZ = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0, 0, angleZ);
    }
}
