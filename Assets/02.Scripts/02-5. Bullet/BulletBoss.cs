using UnityEngine;

public class BulletBoss: Bullet
{
    public override void Move()
    {
        Vector2 direction = Vector2.down;
        transform.Translate(direction * Speed * Time.deltaTime);
    }

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
            BulletPool.Instance.ReturnObject(this);
        }
    }
}
