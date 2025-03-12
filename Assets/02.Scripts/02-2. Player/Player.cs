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
                Destroy(gameObject);
            }
        }
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

        CurrentHealthPoint = MaxHealthPoint;
    }
    public void TakeDamage(int damage)
    {
        CurrentHealthPoint -= damage;
    }
}
