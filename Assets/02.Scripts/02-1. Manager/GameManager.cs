using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int _killedEnemyCount;
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
