using UnityEngine;

public class PlayerStateContext
{
    private IPlayerState _currentState;

    public IPlayerState CurrentState
    {
        get { return _currentState; }
        set { _currentState = value; }
    }
    private PlayerController _playerController;

    public PlayerStateContext(PlayerController controller)
    {
        _playerController = controller;
    }

    public void ChangeState()
    {
        _currentState = new PlayerManualState();
        _currentState.Enter(_playerController);
    }

    public void ChangeState(IPlayerState nextState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = nextState;
        _currentState.Enter(_playerController);
    }
}
