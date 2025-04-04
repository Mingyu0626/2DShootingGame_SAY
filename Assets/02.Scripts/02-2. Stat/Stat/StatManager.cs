using System;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : Singleton<StatManager>
{
    // 관리자: 추가, 삭제, 조회, 정렬, 
    // 레벨업, 조회

    // 데이터
    public List<StatDataSO> StatDataList;

    private List<Stat> _stats = new List<Stat>();
    public List<Stat> Stats => _stats;

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < (int)StatType.Count; ++i)
        {
            _stats.Add(new Stat((StatType)i, 1, StatDataList[i]));
        }
    }


    public bool TryLevelUp(StatType statType)
    {
        return _stats[(int)statType].TryUpgrade();
    }
}