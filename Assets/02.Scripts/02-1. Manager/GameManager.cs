using DG.Tweening;
using NUnit.Framework;
using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{ 
    private PlayData _playData;
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

    protected override void Awake()
    {
        base.Awake();
        _playData = new PlayData(0, 0, 0);
        LoadPlayData();
        _playData.KillCount = 0;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
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
        UI_Game.Instance.InitUI(_playData.KillCount, _playData.Score, _playData.BoomCount);
    }
}
