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

// Enemy Data를 어떻게 관리할 것인가
// 추상 클래스가 아닌 인터페이스를 사용한 순간
// 추상 클래스에는 데이터가 들어갈 수가 없다.
// 그럼 데이터 클래스로 빼서...?