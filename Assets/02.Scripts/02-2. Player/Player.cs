using UnityEngine;


public class Player : MonoBehaviour
{
    // "응집도"는 높이고, "결합도"는 낮춰라
    // 응집도 : 데이터와 관련된 로직이 한 곳에 모여있는 구조 -> 응집도가 높은 구조
    // 결합도 : 두 클래스간에 상호작용 의존 정도
    public static Player Instance { get; private set; }
    [SerializeField] private int _maxHealthPoint = 3;
    [SerializeField] private int _currentHealthPoint;
    [SerializeField] private ShakeCamera _shakeCamera;

    private PlayerData _playerData;
    public PlayerData PlayerData
    {
        get { return _playerData; }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _playerData = GetComponent<PlayerData>();
        _playerData.CurrentHealthPoint = _playerData.MaxHealthPoint;
    }
    public void TakeDamage(int damage)
    {
        _playerData.CurrentHealthPoint -= damage;
        if (!ReferenceEquals(_shakeCamera, null))
        {
            _shakeCamera.Shake(0.5f, 0.3f);
        }
    }
}
