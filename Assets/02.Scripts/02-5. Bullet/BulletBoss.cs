using UnityEngine;

public class BulletBoss: Bullet
{


    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(nameof(Tags.Player)))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                // �÷��̾�� �ִ� �������� ���� ������ ����ü�� �� ���� ����
                player.TakeDamage(Damage);
            }
            Destroy(gameObject);
        }
    }
}
