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

    [Header("Movement")]
    [Tooltip("적의 이동속도")]
    [SerializeField] private float _speed;
    [Tooltip("이동 방향")]
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
    [Tooltip("적이 플레이어에게 가할 수 있는 충돌 데미지")]
    [SerializeField] private int _damage;
    public int Damage
    {
        get { return _damage; }
        private set { _damage = value; }
    }


    [Header("ItemSpawn")]
    [Tooltip("아이템 드롭 확률")]
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
