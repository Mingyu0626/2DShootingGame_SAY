using UnityEngine;

public class EnemyTargetType : Enemy, IEnemyMove
{
    private Transform _playerTransform;
    private Vector3 _directionToPlayer;
    private void OnEnable()
    {
        _playerTransform = Player.Instance.transform;
        _directionToPlayer = _playerTransform.position - transform.position;
        LookAtPlayer(_directionToPlayer);
    }
    private void Update()
    {
        Move(DirectionEnum);
    }
    public void Move(Direction dir)
    {
        // Translate��, ȸ���� �ΰ��ϱ� ������ �� ������ �ʴ´�.
        // Translate��, ������ �ʿ��� �� ���°� ����.
        // transform.Translate(_directionToPlayer.normalized * Speed * Time.deltaTime, Space.World);
        transform.position += (Vector3)(_directionToPlayer.normalized * EnemyData.Speed * Time.deltaTime);
    }
    private void LookAtPlayer(Vector2 distance)
    {
        // �⺻���� �������� �ٶ󺸰� �ֱ� ������ 90���� �����ش�.
        float angleZ = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0, 0, angleZ);
    }
}
