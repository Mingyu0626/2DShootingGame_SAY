using UnityEngine;

public class EnemyData : ScriptableObject
{
    [Header("HP")]
    [Tooltip("���� �ִ� ü��")]
    [SerializeField] private int _maxHealthPoint = 2;
    [Tooltip("���� ���� ü��")]
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
    [Tooltip("���� �÷��̾�� ���� �� �ִ� �浹 ������")]
    [SerializeField] private int _damage;
    public int Damage
    {
        get { return _damage; }
        private set { _damage = value; }
    }


    [Tooltip("���ø� Ÿ���� ���� �� ������ Ÿ�� �� GO")] // EnemySplit Ŭ������ ����
    [SerializeField] private GameObject _enemyZombieGO;


    [Header("ItemSpawn")]
    [Tooltip("������ ��� Ȯ��")]
    [SerializeField] private float _itemSpawnProbability = 0.3f;
    private ItemData _itemData;

    [Header("Effects")]
    [SerializeField] private GameObject _explosionVFXPrefab;

}
