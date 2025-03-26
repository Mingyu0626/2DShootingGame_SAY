using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalType: Enemy, IEnemyMove
{
    private void Update()
    {
        Move(DirectionEnum);
    }
    public void Move(Direction dir)
    {
        Vector2 directionVector = EnemyData.DirectionDictionary[dir];
        // transform.Translate(directionVector * Speed * Time.deltaTime);
        transform.position += (Vector3)(directionVector * EnemyData.Speed * Time.deltaTime);
    }
}