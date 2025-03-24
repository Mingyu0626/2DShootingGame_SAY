using UnityEngine;

public class EnemyShakeType : Enemy, IEnemyMove
{

    [Header("Snake")] // Shake Å¸ÀÔ
    [Tooltip("ÃÑ¾Ë ±ËµµÀÇ ÁÖÆÄ¼ö")]
    [SerializeField] private float _frequency;
    [Tooltip("ÃÑ¾Ë ±ËµµÀÇ ÁøÆø")]
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
