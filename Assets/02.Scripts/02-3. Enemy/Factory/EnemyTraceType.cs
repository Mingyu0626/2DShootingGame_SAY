using UnityEngine;

public class EnemyTraceType : MonoBehaviour, IEnemy, IEnemyMove
{
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private EnemyData _enemyData;

    private Transform _playerTransform;

    public EnemyType EnemyType
    {
        get { return _enemyType; }
        set { _enemyType = value; }
    }
    public void Init()
    {
        EnemyType = _enemyType;
    }
    public void Init(Direction dir)
    {
        EnemyType = _enemyType;
        _enemyData.DirectionEnum = dir;
    }
    private void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector3 directionVector = (_playerTransform.position - transform.position).normalized;
        transform.position += (Vector3)(directionVector * _enemyData.Speed * Time.deltaTime);
        LookAtPlayer(_playerTransform.position - transform.position);
    }
    private void LookAtPlayer(Vector2 distance)
    {
        // �⺻���� �������� �ٶ󺸰� �ֱ� ������ 90���� �����ش�.
        float angleZ = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0, 0, angleZ);
    }
}
