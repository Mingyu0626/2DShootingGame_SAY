using UnityEngine;

public class EnemySplitType : MonoBehaviour, IEnemy, IEnemyMove
{
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private EnemyData _enemyData;

    [Tooltip("���ø� Ÿ���� ���� �� ������ Ÿ�� �� GO")] // EnemySplit Ŭ������ ����
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
