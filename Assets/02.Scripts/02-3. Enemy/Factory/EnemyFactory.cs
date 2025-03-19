using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Normal, // 일반 타입
    Split,  // 파괴 시 3기의 적으로 분리되는 타입
    Shake,  // 곡선 궤적(삼각? 베지어?)으로 움직이는 타입
    Trace,  // 플레이어를 계속 추격하는 타입
    Target, // 생성 시점의 플레이어 위치로 움직이는 타입
}

public abstract class Factory<T> : MonoBehaviour where T : IProduct
{
    public abstract T GetProduct(GameObject productGO, Vector3 position);
}



public class EnemyFactory : Factory<IEnemy>
{
    public override IEnemy GetProduct(GameObject enemyGO, Vector3 position)
    {
        GameObject instance = Instantiate(enemyGO, position,
            Quaternion.identity);
        IEnemy newEnemy = instance.GetComponent<IEnemy>();
        // newEnemy.Init();
        return newEnemy;
    }

}
