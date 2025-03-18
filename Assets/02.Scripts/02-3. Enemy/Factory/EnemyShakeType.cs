using UnityEngine;

public class EnemyShakeType : MonoBehaviour, IEnemy, IEnemyMove
{
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private EnemyData _enemyData;

    [Header("Snake")] // Shake Ÿ��
    [Tooltip("�Ѿ� �˵��� ���ļ�")]
    [SerializeField] private float _frequency;
    [Tooltip("�Ѿ� �˵��� ����")]
    [SerializeField] private float _amplitude;
    public float Frequency
    {
        get { return _frequency; }
        private set { _frequency = value; }
    }
    public float Amplitude
    {
        get { return _amplitude; }
        private set { _amplitude = value; }
    }
    public EnemyType EnemyType
    {
        get { return _enemyType; }
        set { _enemyType = value; }
    }

    public void Init()
    {
        EnemyType = _enemyType;
    }

    public void Move()
    {
        float offset = Mathf.Sin(Time.time * Frequency) * Amplitude;
        transform.position += transform.right * offset * Time.deltaTime;
    }
}
