using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left,
    Right,
    Down
}


public class EnemyData : MonoBehaviour
{
    [Header("Type")]
    [Tooltip("���� Ÿ��")]
    [SerializeField] private EnemyType _enemyType;
    public EnemyType EnemyType { get => _enemyType; }

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
        set
        {
            _currentHealthPoint = Mathf.Clamp(value, 0, _maxHealthPoint);
        }
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
        private set { _speed = value; }
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
        private set { _damage = value; }
    }


    [Header("ItemSpawn")]
    [Tooltip("������ ��� Ȯ��")]
    [SerializeField] private float _itemSpawnProbability = 0.3f;
    public float ItemSpawnProbability 
    { 
        get => _itemSpawnProbability; 
        private set => _itemSpawnProbability = value; 
    }

    [Header("Effects and Animation")]
    [SerializeField] private Animator _enemyAnimator;
    [SerializeField] private GameObject _explosionVFXPrefab;
    public Animator EnemyAnimator 
    { 
        get => _enemyAnimator; 
        set => _enemyAnimator = value; 
    }
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
}
