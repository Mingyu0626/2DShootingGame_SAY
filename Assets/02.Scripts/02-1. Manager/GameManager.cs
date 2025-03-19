using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private UI_Game _gameUI;
    private int _killedEnemyCount;
    private int _score;
    public int KilledEnemyCount 
    { 
        get => _killedEnemyCount;
        set
        {
            _killedEnemyCount = value;
            if (0 < _killedEnemyCount / 20 && _killedEnemyCount % 20 == 0)
            {
                Player.Instance.gameObject.GetComponent<PlayerData>().CurrentBoomCount++;
            }
            _gameUI.RefreshKillCount(_killedEnemyCount);
        }
    }
    public int Score 
    { 
        get => _score;
        set
        {
            _score = value;
            _gameUI.RefreshScore(_score);
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
        DontDestroyOnLoad(gameObject);
    }
}
