using System;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : Singleton<StatManager>
{
    // ������: �߰�, ����, ��ȸ, ����, 
    // ������, ��ȸ

    // ������
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