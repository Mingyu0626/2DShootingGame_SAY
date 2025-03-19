using UnityEngine;

public class EnemyTraceType : Enemy, IEnemy, IEnemyMove
{
    [SerializeField] private EnemyType _enemyType;
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
        EnemyData.DirectionEnum = dir;
    }
    private void Start()
    {
        _playerTransform = Player.Instance.transform;
    }
    private void Update()
    {
        Move(EnemyData.DirectionEnum);
    }
    public void Move(Direction dir)
    {
        Vector3 directionVector = (_playerTransform.position - transform.position).normalized;
        transform.position += (Vector3)(directionVector * EnemyData.Speed * Time.deltaTime);
        LookAtPlayer(_playerTransform.position - transform.position);
    }
    private void LookAtPlayer(Vector2 distance)
    {
        // �⺻���� �������� �ٶ󺸰� �ֱ� ������ 90���� �����ش�.
        float angleZ = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0, 0, angleZ);
    }
}
