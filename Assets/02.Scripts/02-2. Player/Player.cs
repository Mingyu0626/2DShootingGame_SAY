using UnityEngine;


public class Player : MonoBehaviour
{
    // "������"�� ���̰�, "���յ�"�� �����
    // ������ : �����Ϳ� ���õ� ������ �� ���� ���ִ� ���� -> �������� ���� ����
    // ���յ� : �� Ŭ�������� ��ȣ�ۿ� ���� ����
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
