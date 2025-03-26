using TMPro;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class UI_StatButton : MonoBehaviour
{
    public Stat _stat;

    [SerializeField] private TextMeshProUGUI _nameTextUI;
    [SerializeField] private TextMeshProUGUI _valueTextUI;
    [SerializeField] private TextMeshProUGUI _costTextUI;
    [SerializeField] private Sprite _canUpgradeSprite;
    [SerializeField] private Sprite _canNotUpgradeSprite;
    private Image _image;
    private RectTransform _rectTransform;
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }
    public void Refresh()
    {
        _nameTextUI.text = _stat.StatType.ToString();
        _valueTextUI.text = $"{_stat.Value}";
        _costTextUI.text = $"{_stat.Cost:N0}";
        if (CurrencyManager.Instance.Have(CurrencyType.Gold, _stat.Cost))
        {
            _image.sprite = _canUpgradeSprite;
        }
        else
        {
            _image.sprite = _canNotUpgradeSprite;
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
    public void Scaling()
    {
        _rectTransform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 0.08f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                _rectTransform.localScale = Vector3.one;
            });
    }
}