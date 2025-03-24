using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelDataSO : ScriptableObject
{
    [SerializeField] private int _score; // 기준이 되는 점수
    public int Score { get => _score; private set => _score = value; }
    [SerializeField] private float _damageFactor = 1f;
    public float DamageFactor { get => _damageFactor; set => _damageFactor = value; }

    [SerializeField] private float _healthFactor = 1f;
    public float HealthFactor { get => _healthFactor; set => _healthFactor = value; }

    [SerializeField] private float _speedFactor = 1f;
    public float SpeedFactor { get => _speedFactor; set => _speedFactor = value; }


}
