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
        // �⺻���� �������� �ٶ󺸰� �ֱ� ������ 90���� �����ش�.
        float angleZ = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0, 0, angleZ);
    }
}
