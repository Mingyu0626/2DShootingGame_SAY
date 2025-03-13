using UnityEngine;


public class Player : MonoBehaviour
{
    // "응집도"는 높이고, "결합도"는 낮춰라
    // 응집도 : 데이터와 관련된 로직이 한 곳에 모여있는 구조 -> 응집도가 높은 구조
    // 결합도 : 두 클래스간에 상호작용 의존 정도
    public static Player Instance { get; private set; }
    [SerializeField] 
    private int _maxHealthPoint = 3;
    [SerializeField] 
    private int _currentHealthPoint;
    private PlayMode _currentPlayMode;
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
            if (_currentHealthPoint == 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public PlayMode CurrentPlayMode
    {
        get { return _currentPlayMode; }
        set { _currentPlayMode = value; }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // DontDestroyOnLoad(gameObject);

        CurrentPlayMode = PlayMode.Manual;
        CurrentHealthPoint = MaxHealthPoint;
    }
    public void TakeDamage(int damage)
    {
        CurrentHealthPoint -= damage;
    }
}
