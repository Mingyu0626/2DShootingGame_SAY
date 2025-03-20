using System;
using System.Security.Cryptography;
using UnityEngine;

[Serializable]
public class PlayData
{
    public int Score;
    public int KillCount;
    public int BoomCount;

    public PlayData(int score, int killCount, int boomCount)
    {
        Score = score;
        KillCount = killCount;
        BoomCount = boomCount;
    }
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private UI_Game _gameUI;
    private PlayData _playData;
    public UI_Game GameUI { get => _gameUI; private set => _gameUI = value; }
    public PlayData PlayData
    {
        get
        {
            return _playData;
        }
        set
        {
            _playData = value;
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
        _playData = new PlayData(0, 0, 0);
        LoadPlayData();
    }

    private void OnDestroy()
    {
        SavePlayData();
    }

    private void SavePlayData()
    {
        string jsonData = JsonUtility.ToJson(_playData);
        string encryptedData = CryptoUtility.Encrypt(jsonData);
        PlayerPrefs.SetString("PlayerData", encryptedData);
    }

    private void LoadPlayData()
    {
        string encryptedData = PlayerPrefs.GetString("PlayerData", string.Empty);
        if (!string.IsNullOrEmpty(encryptedData))
        {
            string jsonData = CryptoUtility.Decrypt(encryptedData);
            if (!string.IsNullOrEmpty(jsonData))
            {
                _playData = JsonUtility.FromJson<PlayData>(jsonData);
            }
        }
        _gameUI.InitUI(_playData.KillCount, _playData.Score, _playData.BoomCount);
    }
}
