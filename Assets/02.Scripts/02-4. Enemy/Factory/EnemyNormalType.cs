using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalType: Enemy, IEnemy, IEnemyMove
{
    [SerializeField] private EnemyType _enemyType;
    public EnemyType EnemyType 
    { 
        get { return _enemyType; }
        set { _enemyType = value; } 
    }

    public void Init()
    {
        EnemyType = _enemyType;
    }
    public void Init(Direction dir)
    {
        EnemyType = _enemyType;
        EnemyData.DirectionEnum = dir;
    }
    private void Update()
    {
        Move(EnemyData.DirectionEnum);
    }

    public void Move()
    {
        Vector2 directionVector = Vector2.down;
        // transform.Translate(directionVector * Speed * Time.deltaTime);
    }
    public void Move(Direction dir)
    {
        Vector2 directionVector = EnemyData.DirectionDictionary[dir];
        // transform.Translate(directionVector * Speed * Time.deltaTime);
        transform.position += (Vector3)(directionVector * EnemyData.Speed * Time.deltaTime);
    }
}

// Enemy Data�� ��� ������ ���ΰ�
// �߻� Ŭ������ �ƴ� �������̽��� ����� ����
// �߻� Ŭ�������� �����Ͱ� �� ���� ����.
// �׷� ������ Ŭ������ ����...?