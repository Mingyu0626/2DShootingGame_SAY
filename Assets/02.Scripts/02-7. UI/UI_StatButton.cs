using TMPro;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using Coffee.UIExtensions;

public class UI_StatButton : MonoBehaviour
{
    public Stat _stat;

    [SerializeField] private TextMeshProUGUI _nameTextUI;
    [SerializeField] private TextMeshProUGUI _valueTextUI;
    [SerializeField] private TextMeshProUGUI _costTextUI;
    [SerializeField] private Sprite _canUpgradeSprite;
    [SerializeField] private Sprite _canNotUpgradeSprite;
    private RectTransform _rectTransform;
    private Image _image;
    private Button _button;
    private ShinyEffectForUGUI _particleEffect;
    private float _effectDuration = 1f;
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _particleEffect = GetComponent<ShinyEffectForUGUI>();
    }
    public void Refresh()
    {
        _nameTextUI.text = _stat.StatType.ToString();
        _valueTextUI.text = $"{_stat.Value}";
        _costTextUI.text = $"{_stat.Cost:N0}";
        if (CurrencyManager.Instance.Have(CurrencyType.Gold, _stat.Cost))
        {
            _image.sprite = _canUpgradeSprite;
            _button.interactable = true;
        }
        else
        {
            _image.sprite = _canNotUpgradeSprite;
            _button.interactable = false;
        }
    }

    public void OnClickLevelUp()
    {
        if (StatManager.Instance.TryLevelUp(_stat.StatType))
        {
            // 업그레이드 성공 이펙트 실행            
            _particleEffect.Play(_effectDuration);
            UI_Game.Instance.RefreshGold(CurrencyManager.Instance.Gold);
            Scaling();
        }
        else
        {
            Debug.Log($"돈이 부족합니다!");
            // 돈이 부족합니다 토스팝업 Show
        }
    }
    public void Scaling()
    {
        _rectTransform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                _rectTransform.localScale = Vector3.one;
            });
    }
}