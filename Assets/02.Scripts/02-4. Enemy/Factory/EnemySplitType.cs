using UnityEngine;

public class EnemySplitType : Enemy, IEnemy, IEnemyMove
{
    [SerializeField] private EnemyType _enemyType;

    [Tooltip("���ø� Ÿ���� ���� �� ������ Ÿ�� �� GO")]
    [SerializeField] private GameObject _enemyZombieGO;
    [SerializeField] private EnemyFactory _enemyFactory;
    public EnemyType EnemyType
    {
        get { return _enemyType; }
        set { _enemyType = value; }
    }

    public void Init()
    {
        EnemyType = _enemyType;
        EnemyData.DirectionEnum = Direction.Down;
    }
    public void Init(Direction dir)
    {
        EnemyType = _enemyType;
        EnemyData.DirectionEnum = dir;
    }
    private void Update()
    {
        Move(EnemyData.DirectionEnum);
    }
    public void Move(Direction dir)
    {
        Vector2 directionVector = EnemyData.DirectionDictionary[dir];
        // transform.Translate(directionVector * Speed * Time.deltaTime);
        transform.position += (Vector3)(directionVector * EnemyData.Speed * Time.deltaTime);
    }
    private void SplitEnemies()
    {
        foreach (Transform childSpawnPointTransform in transform)
        {
            IEnemy enemyInterface = _enemyFactory.GetProduct(_enemyZombieGO, childSpawnPointTransform.position);
            enemyInterface.Init();
            // Enemy_Split�� �ڽ� GO�� spawnPoint �̿ܿ� �ٸ� GO�� �߰��ȴٸ� ������ �ʿ��ϴ�.
            // Instantiate(_enemyZombieGO, childSpawnPointTransform.position, childSpawnPointTransform.rotation);
        }
    }
}
