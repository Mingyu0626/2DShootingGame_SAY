using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyData _enemyData;
    private ItemData _itemData;
    protected EnemyData EnemyData { get => _enemyData; private set => _enemyData = value; }
    protected ItemData ItemData { get => _itemData; private set => _itemData = value; }

    public void Awake()
    {
        _enemyData = GetComponent<EnemyData>();
        _itemData = GameObject.FindGameObjectWithTag(nameof(Tags.Item)).GetComponent<ItemData>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(nameof(Tags.Player)))
        {
            Player otherPlayer = other.GetComponent<Player>();
            if (otherPlayer != null)
            {
                otherPlayer.TakeDamage(_enemyData.Damage);
                Destroy(gameObject);
            }
        }
    }
    public void TakeDamage(Damage damage)
    {
        PlayHitAnimation();
        _enemyData.CurrentHealthPoint -= damage.Value;
        if (_enemyData.CurrentHealthPoint == 0)
        {
            OnEnemyDeath(damage);
        }
    }
    public void OnEnemyDeath(Damage damage)
    {
        if (damage.Type == DamageType.Bullet)
        {
            int killCount = GameManager.Instance.PlayData.KillCount += 1;
            GameManager.Instance.GameUI.RefreshKillCount(killCount);
            int score = GameManager.Instance.PlayData.Score += _enemyData.Score;
            GameManager.Instance.GameUI.RefreshScore(score);

            if (0 < killCount && killCount % 20 == 0)
            {
                int boomCount = GameManager.Instance.PlayData.BoomCount += 1;
                GameManager.Instance.GameUI.RefreshBoomCount(boomCount);
            }

        }
        SpawnRandomItem();
        InstantiateVFX();
        Destroy(gameObject);
    }

    private void SpawnRandomItem()
    {
        float randomResult = Random.Range(0.0f, 1.0f);
        if (randomResult <= _enemyData.ItemSpawnProbability)
        {
            Instantiate(_itemData.ItemList[Random.Range(0, _itemData.ItemList.Count)],
                transform.position, new Quaternion(0, 0, 0, 0));
        }
    }
    private void InstantiateVFX()
    {
        GameObject vfx = Instantiate(_enemyData.ExplosionVFXPrefab, transform.position, new Quaternion(0, 0, 0, 0));
    }
    private void PlayHitAnimation()
    {
        _enemyData.EnemyAnimator.SetTrigger("HitTrigger");
    }
}
