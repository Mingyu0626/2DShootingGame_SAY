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
    Boss // 100ų �� ��ȯ�Ǵ� ���� Ÿ��
}

public class EnemyFactory : Factory<Enemy>
{
    public override Enemy GetProduct(GameObject enemyGO, Vector3 position)
    {
        return Instantiate(enemyGO, position,Quaternion.identity)
            .GetComponent<Enemy>();
    }

}
