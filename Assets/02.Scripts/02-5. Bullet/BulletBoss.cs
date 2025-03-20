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
                // 플레이어에게 주는 데미지는 아직 데미지 구조체로 안 묶은 상태
                player.TakeDamage(Damage);
            }
            Destroy(gameObject);
        }
    }
}
