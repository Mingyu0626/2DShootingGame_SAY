using System.Collections.Generic;
using UnityEngine;


// ��ũ���ͺ� ������Ʈ�� �����͸� ������ �� �ִ� '������ �����̳�'��.
// ���� ������Ʈ�� ����̳� Ʈ������ ���� �ܼ��� �����͸� ���� �����ϴ�.
// �޸� �Ҹ� ���� �� �ִ�.
// ������ ������ �����̹Ƿ�, �� ����� '�ö��̿���Ʈ' �����̶�� �� �� �ִ�.
// �����͸� ���ȭ �����ν� �׽�Ʈ�� ������ ��������.
[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Type")]
    [Tooltip("���� Ÿ��")]
    [SerializeField] private EnemyType _enemyType;
    public EnemyType EnemyType { get => _enemyType; }

    [Header("HP")]
    [Tooltip("���� �ִ� ü��")]
    [SerializeField] private int _maxHealthPoint = 2;
    public int MaxHealthPoint
    {
        get { return _maxHealthPoint; }
        set { _maxHealthPoint = value; }
    }


    [Header("Movement")]
    [Tooltip("���� �̵��ӵ�")]
    [SerializeField] private float _speed;
    [Tooltip("�̵� ����")]
    [SerializeField]
    private Direction _directionEnum;
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    public Direction DirectionEnum
    {
        get { return _directionEnum; }
        set { _directionEnum = value; }
    }

    [Header("Damage")]
    [Tooltip("���� �÷��̾�� ���� �� �ִ� �浹 ������")]
    [SerializeField] private int _damage;
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }


    [Header("DropTable")]
    [Tooltip("������ ��� Ȯ��")]
    [SerializeField] private float _itemSpawnProbability = 0.3f;
    public float ItemSpawnProbability
    {
        get => _itemSpawnProbability;
        private set => _itemSpawnProbability = value;
    }
    [Tooltip("ȹ�� ���� ���")]
    [SerializeField] private int _earnableGold = 100;
    public int EarnableGold { get => _earnableGold; set => _earnableGold = value; }

    [Header("Effects and Animation")]
    [SerializeField] private GameObject _explosionVFXPrefab;
    public GameObject ExplosionVFXPrefab
    {
        get => _explosionVFXPrefab;
        private set => _explosionVFXPrefab = value;
    }


    [Header("Score")]
    [Tooltip("ȹ�� ����")]
    [SerializeField] private int _score;
    public int Score { get => _score; private set => _score = value; }

    private Dictionary<Direction, Vector2> _directionDictionary = new Dictionary<Direction, Vector2>()
    {
        { Direction.Down, Vector2.down },
        { Direction.Left, Vector2.left},
        { Direction.Right, Vector2.right }
    };
    public Dictionary<Direction, Vector2> DirectionDictionary
    {
        get { return _directionDictionary; }
    }

    [Header("������ ������ �޴� ������")]
    [SerializeField] private int _damageStart;
    [SerializeField] private int _speedStart;
    [SerializeField] private int _maxHealthPointStart;
    public int DamageStart { get => _damageStart; set => _damageStart = value; }
    public int SpeedStart { get => _speedStart; set => _speedStart = value; }
    public int MaxHealthPointStart { get => _maxHealthPointStart; set => _maxHealthPointStart = value; }
}
public enum Direction
{
    Left,
    Right,
    Down
}
