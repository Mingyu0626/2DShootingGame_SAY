using System.Collections.Generic;
using UnityEngine;

public class UI_Stat : MonoBehaviour
{
    public List<UI_StatButton> UI_StatButtons;

    private void Start()
    {
        // 1. ���� ���۵Ǹ� ���� �Ŵ����κ��� ������ ������ ���� ��ư UI���� �ʱ�ȭ �Ѵ�.
        for (int i = 0; i < (int)StatType.Count; ++i)
        {
            UI_StatButtons[i]._stat = StatManager.Instance.Stats[i];
        }

        // 2. ���� �Ŵ������� �����Ͱ� ��ȭ�� ������ ���ΰ�ħ �Լ��� ȣ���ش޶�� ����Ѵ�.
        // ������ - ��Ʃ�� ���� ����
        CurrencyManager.Instance.OnDataChangedCallback += Refresh;

        // ToDo: �ϸ� ����.
        // CurrencyManager.Instance.OnDataChangedCallback += Refresh;

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