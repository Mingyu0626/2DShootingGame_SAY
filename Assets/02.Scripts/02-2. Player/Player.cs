using UnityEngine;


public class Player : MonoBehaviour
{
    // "������"�� ���̰�, "���յ�"�� �����
    // ������ : �����Ϳ� ���õ� ������ �� ���� ���ִ� ���� -> �������� ���� ����
    // ���յ� : �� Ŭ�������� ��ȣ�ۿ� ���� ����
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
