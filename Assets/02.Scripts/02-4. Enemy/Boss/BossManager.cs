using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossManager : Singleton<BossManager>
{
    [Header("Boss")]
    [SerializeField] private List<GameObject> _enemySpawners;
    [SerializeField] private Transform _bossSpawnTransform;
    [SerializeField] private float _precursorTime;
    private bool _isBossSpawned = false;
    public bool IsBossSpawned { get => _isBossSpawned; private set => _isBossSpawned = value; }

    [Header("Warning Animation")]
    [SerializeField] private GameObject _panelWarningTop;
    private Vector3 _panelWarningTopOriginalPosition;
    [SerializeField] private GameObject _panelWarningBottom;
    private Vector3 _panelWarningBottomOriginalPosition;
    [SerializeField] private ShakeCamera _shakeCamera;
    [SerializeField] private float _fadeTime = 0.5f;
    [SerializeField] private float _panelMovingDistance = 1000f;


    private UI_TweeningUtil _tweeningUtil;

    protected override void Awake()
    {
        base.Awake();
        _panelWarningTopOriginalPosition 
            = _panelWarningTop.GetComponent<RectTransform>().anchoredPosition;
        _panelWarningBottomOriginalPosition 
            = _panelWarningBottom.GetComponent<RectTransform>().anchoredPosition;
        _tweeningUtil = new UI_TweeningUtil();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
    public bool CanBossSpawn()
    {
        return 0 < GameManager.Instance.PlayData.KillCount
            && GameManager.Instance.PlayData.KillCount % 100 == 0 && !IsBossSpawned;
    }
    public void SpawnBossCoroutine()
    {
        StartCoroutine(SpawnBoss());
    }
    public IEnumerator SpawnBoss()
    {
        // 모든 스포너 비활성화
        SetSpawnerEnable(false);
        _shakeCamera.Shake(_precursorTime, 0.3f);
        PlayWarningAnimation();

        yield return new WaitForSeconds(_precursorTime);

        EnemyPool.Instance.GetObject(EnemyType.Boss, _bossSpawnTransform.position);
        IsBossSpawned = true;
    }
    public void PlayWarningAnimation()
    {
        Image imageTop = _panelWarningTop.GetComponent<Image>();
        TextMeshProUGUI tmpTop = _panelWarningTop.GetComponentInChildren<TextMeshProUGUI>();
        Image imageBottom = _panelWarningBottom.GetComponent<Image>();
        TextMeshProUGUI tmpBottom = _panelWarningBottom.GetComponentInChildren<TextMeshProUGUI>();

        _panelWarningTop.SetActive(true);
        _panelWarningBottom.SetActive(true);

        imageTop.DOFade(1f, _fadeTime).SetEase(Ease.OutQuad);
        tmpTop.DOFade(1f, _fadeTime).SetEase(Ease.OutQuad);
        imageBottom.DOFade(1f, _fadeTime).SetEase(Ease.OutQuad);
        tmpBottom.DOFade(1f, _fadeTime).SetEase(Ease.OutQuad);


        _panelWarningTop.GetComponent<RectTransform>().DOAnchorPosX(_panelMovingDistance, _precursorTime)
            .OnComplete(() =>
            {
                imageTop.DOFade(0f, _fadeTime).SetEase(Ease.OutQuad);
                tmpTop.DOFade(0f, _fadeTime).SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    _panelWarningTop.GetComponent<RectTransform>().anchoredPosition =
                _panelWarningTopOriginalPosition;
                    _panelWarningTop.SetActive(false);
                });
            });

        _panelWarningBottom.GetComponent<RectTransform>().DOAnchorPosX(-_panelMovingDistance, _precursorTime)
            .OnComplete(() =>
            {
                imageBottom.DOFade(0f, _fadeTime).SetEase(Ease.OutQuad);
                tmpBottom.DOFade(0f, _fadeTime).SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    _panelWarningBottom.GetComponent<RectTransform>().anchoredPosition =
                _panelWarningBottomOriginalPosition;
                    _panelWarningBottom.SetActive(false);
                });

            });
    }

    public void SetSpawnerEnable(bool val)
    {
        IsBossSpawned = !val;
        foreach (GameObject go in _enemySpawners)
        {
            go.SetActive(val);
        }
    }
}
