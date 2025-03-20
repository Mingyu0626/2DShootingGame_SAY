using UnityEngine;

public class BulletPlayer : Bullet
{




    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(nameof(Tags.Enemy)))
        {
            Enemy otherEnemy = other.GetComponent<Enemy>();
            if (otherEnemy != null)
            {
                Damage damage = new Damage
                {
                    Value = Damage,
                    Type = DamageType.Bullet,
                    From = gameObject
                };

                otherEnemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }


}
