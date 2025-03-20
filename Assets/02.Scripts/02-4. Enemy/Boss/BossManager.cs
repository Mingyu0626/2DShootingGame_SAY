using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BossManager : Singleton<BossManager>
{
    [Header("Boss")]
    [SerializeField] private List<GameObject> _enemySpawners;
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private float _precursorTime;
    private bool _isBossSpawned = false;

    [Header("Warning Animation")]
    [SerializeField] private GameObject _panelWarningTop;
    private Vector3 _panelWarningTopOriginalPosition;
    [SerializeField] private GameObject _panelWarningBottom;
    private Vector3 _panelWarningBottomOriginalPosition;
    [SerializeField] private ShakeCamera _shakeCamera;


    protected override void Awake()
    {
        base.Awake();
        _panelWarningTopOriginalPosition 
            = _panelWarningTop.GetComponent<RectTransform>().anchoredPosition;
        _panelWarningBottomOriginalPosition 
            = _panelWarningBottom.GetComponent<RectTransform>().anchoredPosition;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
    public bool CanBossSpawn()
    {
        return 0 < GameManager.Instance.PlayData.KillCount
            && GameManager.Instance.PlayData.KillCount % 100 == 0 && !_isBossSpawned;
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

        Instantiate(_bossPrefab);
        _isBossSpawned = true;
    }
    public void PlayWarningAnimation()
    {
        _panelWarningTop.SetActive(true);
        _panelWarningBottom.SetActive(true);

        _panelWarningTop.GetComponent<RectTransform>().DOAnchorPosX(1000f, _precursorTime)
            .OnComplete(() =>
            {
                _panelWarningTop.SetActive(false);
            }).
            OnKill(() =>
            {
                _panelWarningTop.GetComponent<RectTransform>().anchoredPosition = 
                _panelWarningTopOriginalPosition;
            });

        _panelWarningBottom.GetComponent<RectTransform>().DOAnchorPosX(-1000f, _precursorTime)
            .OnComplete(() =>
            {
                _panelWarningBottom.SetActive(false);
            }).
            OnKill(() =>
            {
                _panelWarningBottom.GetComponent<RectTransform>().anchoredPosition =
                _panelWarningBottomOriginalPosition;
            });

    }

    public void SetSpawnerEnable(bool val)
    {
        _isBossSpawned = !val;
        foreach (GameObject go in _enemySpawners)
        {
            go.SetActive(val);
        }
    }
}
