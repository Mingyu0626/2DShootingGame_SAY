using System.Collections.Generic;
using UnityEngine;

public class UI_Stat : MonoBehaviour
{
    public List<UI_StatButton> UI_StatButtons;

    private void Start()
    {
        // 1. 게임 시작되면 스탯 매니저로부터 스탯을 가져와 하위 버튼 UI들을 초기화 한다.
        for (int i = 0; i < (int)StatType.Count; ++i)
        {
            UI_StatButtons[i]._stat = StatManager.Instance.Stats[i];
        }
        CurrencyManager.Instance.OnDataChanged += Refresh;

        Refresh();
    }
    public void Refresh()
    {
        for (int i = 0; i < (int)StatType.Count; ++i)
        {
            UI_StatButtons[i].Refresh();
        }
    }
}