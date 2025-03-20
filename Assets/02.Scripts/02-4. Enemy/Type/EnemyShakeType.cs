using UnityEngine;

public class EnemyShakeType : Enemy, IEnemy, IEnemyMove
{
    [SerializeField] private EnemyType _enemyType;

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
    public void Init(Direction dir)
    {
        EnemyType = _enemyType;
        EnemyData.DirectionEnum = dir;
    }
    private void Update()
    {
        Move(EnemyData.DirectionEnum);
        SinCurve();
    }
    public void Move(Direction dir)
    {
        Vector2 directionVector = EnemyData.DirectionDictionary[dir];
        // transform.Translate(directionVector * Speed * Time.deltaTime);
        transform.position += (Vector3)(directionVector * EnemyData.Speed * Time.deltaTime);
    }
    public void SinCurve()
    {
        float offset = Mathf.Sin(Time.time * Frequency) * Amplitude;
        transform.position += transform.right * offset * Time.deltaTime;
    }
}
