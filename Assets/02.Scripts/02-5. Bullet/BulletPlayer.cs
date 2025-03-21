using UnityEngine;

public class BulletPlayer : Bullet
{
    public override void Move()
    {
        Vector2 direction = Vector2.up;
        transform.Translate(direction * Speed * Time.deltaTime);
    }

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
