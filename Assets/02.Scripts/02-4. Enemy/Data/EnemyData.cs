using System.Collections.Generic;
using UnityEngine;


// 스크립터블 오브젝트는 데이터를 저장할 수 있는 '데이터 컨테이너'다.
// 게임 오브젝트의 기능이나 트랜스폼 없이 단순히 데이터만 저장 가능하다.
// 메모리 소모를 줄일 수 있다.
// 공유된 데이터 개념이므로, 이 방식은 '플라이웨이트' 패턴이라고 할 수 있다.
// 데이터를 모듈화 함으로써 테스트와 관리가 편리해진다.
[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Type")]
    [Tooltip("적의 타입")]
    [SerializeField] private EnemyType _enemyType;
    public EnemyType EnemyType { get => _enemyType; }

    [Header("HP")]
    [Tooltip("적의 최대 체력")]
    [SerializeField] private int _maxHealthPoint = 2;
    public int MaxHealthPoint
    {
        get { return _maxHealthPoint; }
        set { _maxHealthPoint = value; }
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
        set { _speed = value; }
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
        set { _damage = value; }
    }


    [Header("DropTable")]
    [Tooltip("아이템 드롭 확률")]
    [SerializeField] private float _itemSpawnProbability = 0.3f;
    public float ItemSpawnProbability
    {
        get => _itemSpawnProbability;
        private set => _itemSpawnProbability = value;
    }
    [Tooltip("획득 가능 골드")]
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
    [Tooltip("획득 점수")]
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

    [Header("레벨에 영향을 받는 데이터")]
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
