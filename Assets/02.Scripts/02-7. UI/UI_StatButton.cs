using TMPro;
using UnityEngine;

public class UI_StatButton : MonoBehaviour
{
    public Stat _stat;

    [SerializeField] private TextMeshProUGUI _nameTextUI;
    [SerializeField] private TextMeshProUGUI _valueTextUI;
    [SerializeField] private TextMeshProUGUI _costTextUI;
    public void Refresh()
    {
        _nameTextUI.text = _stat.StatType.ToString();
        _valueTextUI.text = $"{_stat.Value}";
        _costTextUI.text = $"{_stat.Cost:N0}";
        if (CurrencyManager.Instance.Have(CurrencyType.Gold, _stat.Cost))
        {
            _costTextUI.color = Color.black;
        }
        else
        {
            _costTextUI.color = Color.red;
        }
    }

    public void OnClickLevelUp()
    {
        if (StatManager.Instance.TryLevelUp(_stat.StatType))
        {
            Debug.Log($"{_stat.StatType} 레벨업!");
            // 업그레이드 성공 이펙트 실행            
        }
        else
        {
            Debug.Log($"돈이 부족합니다!");
            // 돈이 부족합니다 토스팝업 Show
        }
    }
}