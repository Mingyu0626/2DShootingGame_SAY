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
                // 플레이어에게 주는 데미지는 아직 데미지 구조체로 안 묶은 상태
                player.TakeDamage(Damage);
            }
            BulletPool.Instance.ReturnObject(this);
        }
    }
}
