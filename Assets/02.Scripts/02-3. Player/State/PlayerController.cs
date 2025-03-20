using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayMode
{
    Auto,
    Manual,
}


public class PlayerController : MonoBehaviour
{
    private IPlayerState _manualState, _autoState;
    public IPlayerState ManualState
    {
        get { return _manualState; }
        set { _manualState = value; }
    }
    public IPlayerState AutoState
    {
        get { return _autoState; }
        set { _autoState = value; }
    }

    private PlayerStateContext _playerStateContext;
    public PlayerStateContext PlayerStateContext
    {
        get { return _playerStateContext; }
        set { _playerStateContext = value; }
    }

    private PlayMode _currentPlayMode;
    public PlayMode CurrentPlayMode
    {
        get { return _currentPlayMode; }
        set { _currentPlayMode = value; }
    }


    // 데이터 클래스로 따로 빼니까 훨 보기좋네
    private PlayerData _playerData;
    public PlayerData PlayerData
    {
        get { return _playerData; }
        private set { _playerData = value; }
    }

    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
    }

    private void Start()
    {
        _playerStateContext = new PlayerStateContext(this);
        _manualState = new PlayerManualState();
        _autoState = new PlayerAutoState();
        _playerStateContext.ChangeState(_manualState);
    }
    private void Update()
    {
        _playerStateContext.CurrentState.Update();
        if (Input.GetKeyDown(KeyCode.Alpha3) && 0 < GameManager.Instance.PlayData.BoomCount)
        {
            ActivateBoom();
        }
    }
    public void StartStateCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    public void StopStateCoroutine(IEnumerator coroutine)
    {
        StopCoroutine(coroutine);
    }

    public void InstantiateBullet(GameObject bullet, Vector3 position, Quaternion rotation)
    {
        Instantiate(bullet, position, rotation);
    }

    public void ActivateBoom()
    {
        int boomCount = GameManager.Instance.PlayData.BoomCount -= 1;
        GameManager.Instance.GameUI.RefreshBoomCount(boomCount);
        if (_playerData.BoomPrefab == null) return;
        _playerData.BoomPrefab.SetActive(true);
    }
}
