using UnityEngine;

public class EnemyTargetType : Enemy, IEnemy, IEnemyMove
{
    private Transform _playerTransform;
    private Vector3 _directionToPlayer;

    public void Init()
    {
        EnemyData.DirectionEnum = Direction.Down;
    }
    public void Init(Direction dir)
    {
        EnemyData.DirectionEnum = dir;
    }
    private void Start()
    {
        _playerTransform = Player.Instance.transform;
        _directionToPlayer = _playerTransform.position - transform.position;
        LookAtPlayer(_directionToPlayer);
    }
    private void Update()
    {
        Move(EnemyData.DirectionEnum);
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
