using UnityEngine;

public class EnemySplitType : MonoBehaviour, IEnemy, IEnemyMove
{
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private EnemyData _enemyData;

    [Tooltip("스플릿 타입이 죽을 때 생성될 타입 적 GO")] // EnemySplit 클래스로 빼고
    [SerializeField] private GameObject _enemyZombieGO;
    public EnemyType EnemyType
    {
        get { return _enemyType; }
        set { _enemyType = value; }
    }



    public void Init()
    {
        EnemyType = _enemyType;
    }

    public void Move()
    {
    }
}
