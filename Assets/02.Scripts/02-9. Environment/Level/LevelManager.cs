using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<LevelDataSO> _levelDatas;
    public List<LevelDataSO> LevelDatas 
    { get => _levelDatas; private set => _levelDatas = value; }

    protected override void Awake()
    {
        base.Awake();
    }

    public LevelDataSO GetLevelData()
    {
        int score = GameManager.Instance.PlayData.Score;
        foreach (LevelDataSO levelData in _levelDatas)
        {
            if (score < levelData.Score)
            {
                return levelData;
            }
        }
        return _levelDatas[_levelDatas.Count - 1];
    }
}
