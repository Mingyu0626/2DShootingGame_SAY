using UnityEngine;

public class Enemy : MonoBehaviour, IProduct
{
    [SerializeField] private EnemyData _enemyData;
    protected EnemyData EnemyData { get => _enemyData; private set => _enemyData = value; }
    private ItemData _itemData;
    protected ItemData ItemData { get => _itemData; private set => _itemData = value; }
    [Header("HP")]
    [Tooltip("적의 현재 체력")]
    [SerializeField] private int _currentHealthPoint;
    public int CurrentHealthPoint
    {
        get { return _currentHealthPoint; }
        set
        {
            _currentHealthPoint = Mathf.Clamp(value, 0, _enemyData.MaxHealthPoint);
        }
    }
    [Header("Effects and Animation")]
    private Animator _enemyAnimator;
    public Animator EnemyAnimator
    {
        get => _enemyAnimator;
        set => _enemyAnimator = value;
    }
    protected virtual void Awake()
    {
        _itemData = GameObject.FindGameObjectWithTag(nameof(Tags.Item)).GetComponent<ItemData>();
        _enemyAnimator = GetComponent<Animator>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(nameof(Tags.Player)))
        {
            Player otherPlayer = other.GetComponent<Player>();
            PlayerController playerController
                = other.GetComponent<PlayerController>();
            if (!ReferenceEquals(otherPlayer, null) && !ReferenceEquals(playerController, null))
            {
                if (playerController.CurrentPlayMode == PlayMode.Invincible)
                {
                    OnEnemyDeath
                    (new Damage(CurrentHealthPoint,
                        DamageType.InvincibleHeadBut,otherPlayer.gameObject));
                }
                else
                {
                    otherPlayer.TakeDamage(_enemyData.Damage);
                    EnemyPool.Instance.ReturnObject(this);
                }
            }
        }
    }
    public void Init()
    {
        CurrentHealthPoint = _enemyData.MaxHealthPoint;
        //LevelDataSO levelData = LevelManager.Instance.GetLevelData();
        //_enemyData.Damage *= (int)levelData.DamageFactor;
        //_enemyData.Speed *= (int)levelData.SpeedFactor;
        //_enemyData.MaxHealthPoint *= (int)levelData.HealthFactor;
    }
    public void InitDirection(Direction dir)
    {
        _enemyData.DirectionEnum = dir;
    }
    public void TakeDamage(Damage damage)
    {
        PlayHitAnimation();
        CurrentHealthPoint -= damage.Value;
        if (_enemyData.EnemyType == EnemyType.Boss)
        {
            UI_Game.Instance.RefreshBossUI(CurrentHealthPoint);
        }
        Debug.Log(CurrentHealthPoint);
        if (CurrentHealthPoint == 0)
        {
            OnEnemyDeath(damage);
        }
    }
    public void OnEnemyDeath(Damage damage)
    {

        int killCount = GameManager.Instance.PlayData.KillCount += 1;
        int score = GameManager.Instance.PlayData.Score += _enemyData.Score;
        CheckBoomCountGetable(killCount);
        int boomCount = GameManager.Instance.PlayData.BoomCount;
        int gold = GameManager.Instance.PlayData.Gold += _enemyData.EarnableGold;
        CheckCanBossSpawn();
        SpawnRandomItem();
        InstantiateVFX();

        UI_Game.Instance.OnEnemyKilled?.Invoke(killCount, score, boomCount, gold);


        if (EnemyPool.Instance.CheckTypeInPool(_enemyData.EnemyType))
        {
            EnemyPool.Instance.ReturnObject(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void CheckCanBossSpawn()
    {
        if (BossManager.Instance.CanBossSpawn())
        {
            BossManager.Instance.SpawnBossCoroutine();
        }
    }
    private void CheckBoomCountGetable(int killCount)
    {
        if (0 < killCount && killCount % 20 == 0 && GameManager.Instance.PlayData.BoomCount < 3)
        {
            int boomCount = GameManager.Instance.PlayData.BoomCount += 1;
            UI_Game.Instance.RefreshBoomCount(boomCount);
        }
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
        VFXType vfxType = _enemyData.ExplosionVFXPrefab.GetComponent<VFX>().VfxType;
        VFXPool.Instance.GetObject(vfxType, transform.position);
    }
    private void PlayHitAnimation()
    {
        _enemyAnimator.SetTrigger("HitTrigger");
    }
}
