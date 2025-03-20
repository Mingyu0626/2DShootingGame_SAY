using UnityEngine;

public class EnemySplitType : Enemy, IEnemy, IEnemyMove
{

    [Tooltip("���ø� Ÿ���� ���� �� ������ Ÿ�� �� GO")]
    [SerializeField] private GameObject _enemyZombieGO;
    [SerializeField] private EnemyFactory _enemyFactory;
    public void Init()
    {
        EnemyData.DirectionEnum = Direction.Down;
    }
    public void Init(Direction dir)
    {
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
