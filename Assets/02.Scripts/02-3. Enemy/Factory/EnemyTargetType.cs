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
        // Translate는, 회전에 민감하기 때문에 잘 사용되지 않는다.
        // Translate는, 조향이 필요할 때 쓰는게 좋다.
        // transform.Translate(_directionToPlayer.normalized * Speed * Time.deltaTime, Space.World);
        transform.position += (Vector3)(_directionToPlayer.normalized * _enemyData.Speed * Time.deltaTime);
    }
}
