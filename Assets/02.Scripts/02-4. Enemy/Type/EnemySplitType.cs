using UnityEngine;

public class EnemySplitType : Enemy, IEnemyMove
{

    [Tooltip("���ø� Ÿ���� ���� �� ������ ���� Ÿ��")]
    [SerializeField] private EnemyType _splitedEnemyType;
    private void Update()
    {
        Move(DirectionEnum);
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
            Enemy enemy = EnemyPool.Instance.GetObject(_splitedEnemyType, childSpawnPointTransform.position);
            enemy.Init();
            // Enemy_Split�� �ڽ� GO�� spawnPoint �̿ܿ� �ٸ� GO�� �߰��ȴٸ� ������ �ʿ��ϴ�.
            // Instantiate(_enemyZombieGO, childSpawnPointTransform.position, childSpawnPointTransform.rotation);
        }
    }
}
