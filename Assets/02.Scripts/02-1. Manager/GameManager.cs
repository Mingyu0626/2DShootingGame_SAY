using JetBrains.Annotations;
using System;
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
        // PlayerPrefs : 값을 Key, Value 형태로 저장하는 클래스
        // 저장할 수 있는 자료형은 int, float, string
        // 각각 쌍으로 저장(Set), 불러오기(Get) 메서드가 내장되어 있다.
        Debug.Log("세이브 플레이데이터 호출");
        string jsonData = JsonUtility.ToJson(_playData);
        PlayerPrefs.SetString("PlayerData", jsonData);
        PlayerPrefs.Save();
    }

    private void LoadPlayData()
    {
        Debug.Log("로드 플레이데이터 호출");
        string jsonString = PlayerPrefs.GetString("PlayerData", JsonUtility.ToJson(new PlayData(0, 0, 0)));
        _playData = JsonUtility.FromJson<PlayData>(jsonString);
        _gameUI.InitUI(_playData.KillCount, _playData.Score, _playData.BoomCount);
    }

}
