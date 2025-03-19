using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UI_Game : MonoBehaviour
{
    [SerializeField] private List<GameObject> _boomList;
    [SerializeField] private TextMeshProUGUI _killCountText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    private void Awake()
    {
        
    }
    private void Update()
    {
        
    }

    public void RefreshBoomCount(int boomCount)
    {
        for (int i = 0; i < _boomList.Count; i++)
        {
            _boomList[i].SetActive(i < boomCount);
        }
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

}
