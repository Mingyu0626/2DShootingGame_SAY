using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Normal, // �Ϲ� Ÿ��
    Split,  // �ı� �� 3���� ������ �и��Ǵ� Ÿ��
    Shake,  // � ����(�ﰢ? ������?)���� �����̴� Ÿ��
    Trace,  // �÷��̾ ��� �߰��ϴ� Ÿ��
    Target, // ���� ������ �÷��̾� ��ġ�� �����̴� Ÿ��
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
