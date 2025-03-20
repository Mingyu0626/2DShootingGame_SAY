using UnityEngine;

public class EnemySplitType : Enemy, IEnemy, IEnemyMove
{

    [Tooltip("스플릿 타입이 죽을 때 생성될 타입 적 GO")]
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
            // Enemy_Split의 자식 GO로 spawnPoint 이외에 다른 GO가 추가된다면 개선이 필요하다.
            // Instantiate(_enemyZombieGO, childSpawnPointTransform.position, childSpawnPointTransform.rotation);
        }
    }
}
