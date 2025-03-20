using UnityEngine;

public interface IEnemyMove
{
    public void Move(Direction dir = Direction.Down);
}
