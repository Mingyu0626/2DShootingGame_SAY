using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(nameof(Tags.Bullet)))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            BulletPool.Instance.ReturnObject(bullet);
        }
        else if (other.CompareTag(nameof(Tags.Enemy)))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            EnemyPool.Instance.ReturnObject(enemy);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
