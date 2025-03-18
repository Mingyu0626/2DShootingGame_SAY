using UnityEngine;

public class EnemyData : ScriptableObject
{
    [Header("HP")]
    [Tooltip("적의 최대 체력")]
    [SerializeField] private int _maxHealthPoint = 2;
    [Tooltip("적의 현재 체력")]
    [SerializeField] private int _currentHealthPoint;
    public int MaxHealthPoint
    {
        get { return _maxHealthPoint; }
        private set { _maxHealthPoint = value; }
    }
    public int CurrentHealthPoint
    {
        get { return _currentHealthPoint; }
        private set
        {
            _currentHealthPoint = Mathf.Clamp(value, 0, _maxHealthPoint);
        }
    }

    [Header("Damage")]
    [Tooltip("적이 플레이어에게 가할 수 있는 충돌 데미지")]
    [SerializeField] private int _damage;
    public int Damage
    {
        get { return _damage; }
        private set { _damage = value; }
    }


    [Tooltip("스플릿 타입이 죽을 때 생성될 타입 적 GO")] // EnemySplit 클래스로 빼고
    [SerializeField] private GameObject _enemyZombieGO;


    [Header("ItemSpawn")]
    [Tooltip("아이템 드롭 확률")]
    [SerializeField] private float _itemSpawnProbability = 0.3f;
    private ItemData _itemData;

    [Header("Effects")]
    [SerializeField] private GameObject _explosionVFXPrefab;

}
