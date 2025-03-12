using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Normal, // �Ϲ� Ÿ��
    Split,  // �ı� �� 3���� ������ �и��Ǵ� Ÿ��
    Shake,  // �ﰢ �Լ� � �������� �����̴� Ÿ��
    Trace,  // �÷��̾ ��� �߰��ϴ� Ÿ��
    Target, // ���� ������ �÷��̾� ��ġ�� �����̴� Ÿ��
    Bazier // ������ � �������� �����̴� Ÿ��
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealthPoint = 2;
    [SerializeField] private int _currentHealthPoint;
    [SerializeField] private int _damage;
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
            if (_currentHealthPoint == 0)
            {
                OnEnemyDestroyed();
            }
        }
    }
    public int Damage
    {
        get { return _damage; }
        private set { _damage = value; }
    }

    [Header("Type")]
    [Tooltip("���� Ÿ��")]
    [SerializeField] private EnemyType _currentEnemyType;
    [Tooltip("���ø� Ÿ���� ���� �� ������ Ÿ�� �� GO")]
    [SerializeField] private GameObject _enemyZombieGO;
    public EnemyType CurrentEnemyType
    {
        get { return _currentEnemyType; }
        set { _currentEnemyType = value; }
    }
    private void Awake()
    {
        CurrentHealthPoint = MaxHealthPoint;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player otherPlayer = other.GetComponent<Player>();
            if (otherPlayer != null)
            {
                otherPlayer.TakeDamage(Damage);
                CurrentHealthPoint = 0;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        CurrentHealthPoint -= damage;
    }
    private void OnEnemyDestroyed()
    {
        switch (CurrentEnemyType)
        {
            case EnemyType.Normal:
                {
                    break;
                }
            case EnemyType.Split:
                {
                    SplitEnemies();
                    break;
                }
            case EnemyType.Trace:
                {
                    break;
                }
            case EnemyType.Target:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
        Destroy(gameObject);
    }

    private void SplitEnemies()
    {
        foreach (Transform childSpawnPointTransform in transform)
        {
            // Enemy_Split�� �ڽ� GO�� spawnPoint �̿ܿ� �ٸ� GO�� �߰��ȴٸ� ������ �ʿ��ϴ�.
            Instantiate(_enemyZombieGO, childSpawnPointTransform.position,
                childSpawnPointTransform.rotation);
        }
    }
}