using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


public class UI_Game : Singleton<UI_Game>
{
    [SerializeField] private List<GameObject> _boomList;
    [SerializeField] private TextMeshProUGUI _killCountText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameObject _bossHealthGameObject;
    private Slider _bossHealthSlider;
    private Image _bossHealthImage;
    private TextMeshProUGUI _bossHealthText;
    [SerializeField] private float _refreshDelay;
    protected override void Awake()
    {
        base.Awake();
        if (!ReferenceEquals(_bossHealthGameObject, null))
        {
            _bossHealthSlider = _bossHealthGameObject.GetComponent<Slider>();
            _bossHealthImage = _bossHealthSlider.fillRect.GetComponent<Image>();
            _bossHealthText = _bossHealthGameObject.GetComponentInChildren<TextMeshProUGUI>();
        }
    }
    public void InitUI(int killCount, int score, int boomCount)
    {
        RefreshKillCount(killCount);
        RefreshScore(score);
        RefreshBoomCount(boomCount);
    }
    public void RefreshKillCount(int killCount)
    {
        _killCountText.text = $"Kills : {killCount}";
    }

    public void RefreshScore(int score)
    {
        _scoreText.text = score.ToString("N0");
        _scoreText.rectTransform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 0.08f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                _scoreText.rectTransform.localScale = Vector3.one;
            });
    }
    public void RefreshBoomCount(int boomCount)
    {
        for (int i = 0; i < _boomList.Count; i++)
        {
            _boomList[i].SetActive(i < boomCount);
        }
    }

    public void RefreshBossUI(int hp)
    {
        RefreshBossHPValue(hp);
        RefreshBossHPColor(hp);
        RefreshBossHPText(hp);
    }
    private void RefreshBossHPValue(int hp)
    {
        _bossHealthGameObject.GetComponent<Slider>().
            DOValue(hp, _refreshDelay).SetEase(Ease.Linear);
    }
    private void RefreshBossHPColor(int hp)
    {
        Color currentColor = _bossHealthImage.color;
        Color targetColor;
        if (50 <= hp)
        {
            float rValue = Mathf.Lerp(0f, 1f, (100f - hp) / 50f);
            targetColor = new Color(rValue, currentColor.g, currentColor.b, currentColor.a);
        }
        else
        {
            float gValue = Mathf.Lerp(0f, 1f, hp / 50f);
            targetColor = new Color(currentColor.r, gValue, currentColor.b, currentColor.a);
        }
        _bossHealthImage.DOColor(targetColor, _refreshDelay).SetEase(Ease.Linear);
        _bossHealthText.DOColor(targetColor, _refreshDelay).SetEase(Ease.Linear);
    }
    private void RefreshBossHPText(int hp)
    {
        _bossHealthText.text = $"Boss HP : {hp}/100";
    }
    public void InitBossHPSlider(int bossMaxHp)
    {
        _bossHealthSlider.maxValue = bossMaxHp;
        _bossHealthSlider.value = bossMaxHp;
    }
    public void SetBossHPSliderEnable(bool val)
    {
        _bossHealthGameObject.gameObject.SetActive(val);
    }
}
