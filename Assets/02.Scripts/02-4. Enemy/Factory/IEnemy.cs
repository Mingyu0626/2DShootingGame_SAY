using UnityEngine;

public interface IEnemy : IProduct
{
    public EnemyType EnemyType { get; set; }
    public void Init(Direction dir);
}
