using System;
using System.Collections.Generic;
using UnityEngine;

public enum CurrencyType
{
    Gold,
    Diamond,
    Count
}

[Serializable]
public class CurrencySaveData
{
    public List<int> Values = new List<int>(new int[(int)CurrencyType.Count]);

    // ToDo : 저장 시간 추가
}


// 재화 관리자
public class CurrencyManager : Singleton<CurrencyManager>
{
    private CurrencySaveData _saveData;
    private List<int> _values => _saveData.Values;
    public int Gold => _values[(int)CurrencyType.Gold];
    public int Diamond => _values[(int)CurrencyType.Diamond];

    private const string SAVE_KEY = "Currency";

    // 데이터 변경시 호출되는 콜백
    public Action OnDataChanged;

    protected override void Awake()
    {
        base.Awake();
        Load();
    }
    private void Start()
    {
        OnDataChanged += () => UI_Game.Instance.RefreshGold(Gold);
        OnDataChanged += () => UI_Game.Instance.RefreshDiamond(Diamond);
    }
    public int Get(CurrencyType currencyType)
    {
        return _values[(int)currencyType];
    }
    public void Add(CurrencyType currencyType, int amount)
    {
        _values[(int)currencyType] += amount;
        Save();
        OnDataChanged?.Invoke();
    }
    public bool TryConsume(CurrencyType currencyType, int amount)
    {
        if (!Have(currencyType, amount))
        {
            return false;
        }
        _values[(int)currencyType] -= amount;
        Save();
        OnDataChanged?.Invoke();
        return true;
    }
    public bool Have(CurrencyType currencyType, int amount)
    {
        return _values[(int)currencyType] >= amount;
    }
    private void Save()
    {
        string jsonData = JsonUtility.ToJson(_saveData);
        PlayerPrefs.SetString(SAVE_KEY, jsonData);
    }
    private void Load()
    {
        // PlayerPrefs.DeleteKey(SAVE_KEY);
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string jsonData = PlayerPrefs.GetString(SAVE_KEY);
            _saveData = JsonUtility.FromJson<CurrencySaveData>(jsonData);
        }
        else
        {
            _saveData = new CurrencySaveData();
        }
    }
}
