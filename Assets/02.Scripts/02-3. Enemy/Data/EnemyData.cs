using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left,
    Right,
    Down
}


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
    private ItemData _itemData;

    [Header("Effects")]
    [SerializeField] private GameObject _explosionVFXPrefab;


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
