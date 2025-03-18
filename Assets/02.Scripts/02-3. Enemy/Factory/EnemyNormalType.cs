using UnityEngine;

public class EnemyNormalType: MonoBehaviour, IEnemy, IEnemyMove
{
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private EnemyData _enemyData;
    
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
        Vector2 directionVector = Vector2.down;
        // transform.Translate(directionVector * Speed * Time.deltaTime);
    }
}

// Enemy Data�� ��� ������ ���ΰ�
// �߻� Ŭ������ �ƴ� �������̽��� ����� ����
// �߻� Ŭ�������� �����Ͱ� �� ���� ����.
// �׷� ������ Ŭ������ ����...?