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
        // PlayerPrefs : ���� Key, Value ���·� �����ϴ� Ŭ����
        // ������ �� �ִ� �ڷ����� int, float, string
        // ���� ������ ����(Set), �ҷ�����(Get) �޼��尡 ����Ǿ� �ִ�.
        Debug.Log("���̺� �÷��̵����� ȣ��");
        string jsonData = JsonUtility.ToJson(_playData);
        PlayerPrefs.SetString("PlayerData", jsonData);
        PlayerPrefs.Save();
    }

    private void LoadPlayData()
    {
        Debug.Log("�ε� �÷��̵����� ȣ��");
        string jsonString = PlayerPrefs.GetString("PlayerData", JsonUtility.ToJson(new PlayData(0, 0, 0)));
        _playData = JsonUtility.FromJson<PlayData>(jsonString);
        _gameUI.InitUI(_playData.KillCount, _playData.Score, _playData.BoomCount);
    }

}
