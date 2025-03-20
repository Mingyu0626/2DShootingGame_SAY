using System.Collections;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private float _boomDuration;

    private void Awake()
    {
        _boomDuration = Player.Instance.gameObject.GetComponent<PlayerData>().BoomDuration;
    }

    private void OnEnable()
    {
        StartCoroutine(Timer());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Damage damage = new Damage
            {
                Value = 10,
                Type = DamageType.Boom,
                From = gameObject
            };
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(_boomDuration);
        gameObject.SetActive(false);
    }
}
