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
    [SerializeField] private Slider _bossHealthSlider;

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
    public void RefreshBossHP(int hp)
    {
        _bossHealthSlider.value = hp;
        
    }
    public void InitBossHPSlider(int bossMaxHp)
    {
        _bossHealthSlider.maxValue = bossMaxHp;
        _bossHealthSlider.value = bossMaxHp;
    }
    public void SetBossHPSliderEnable(bool val)
    {
        _bossHealthSlider.gameObject.SetActive(val);
    }
}
