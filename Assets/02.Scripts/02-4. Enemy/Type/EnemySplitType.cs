using UnityEngine;

public class EnemySplitType : Enemy, IEnemyMove
{

    [Tooltip("스플릿 타입이 죽을 때 생성될 적의 타입")]
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
            // Enemy_Split의 자식 GO로 spawnPoint 이외에 다른 GO가 추가된다면 개선이 필요하다.
            // Instantiate(_enemyZombieGO, childSpawnPointTransform.position, childSpawnPointTransform.rotation);
        }
    }
}
