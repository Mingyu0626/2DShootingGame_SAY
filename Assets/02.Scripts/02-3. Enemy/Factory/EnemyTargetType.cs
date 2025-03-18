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
    }
    public void Move()
    {
        // Translate��, ȸ���� �ΰ��ϱ� ������ �� ������ �ʴ´�.
        // Translate��, ������ �ʿ��� �� ���°� ����.
        // transform.Translate(_directionToPlayer.normalized * Speed * Time.deltaTime, Space.World);
        transform.position += (Vector3)(_directionToPlayer.normalized * _enemyData.Speed * Time.deltaTime);
    }
}
