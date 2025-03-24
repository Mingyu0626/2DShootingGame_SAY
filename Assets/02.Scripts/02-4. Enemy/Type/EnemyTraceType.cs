using UnityEngine;

public class EnemyTraceType : Enemy, IEnemyMove
{
    private Transform _playerTransform;
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
        // 기본값이 오른쪽을 바라보고 있기 때문에 90도를 더해준다.
        float angleZ = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0, 0, angleZ);
    }
}
