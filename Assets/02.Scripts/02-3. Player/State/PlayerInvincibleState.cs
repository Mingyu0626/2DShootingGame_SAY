using UnityEngine;

public class PlayerInvincibleState : IPlayerState, IMove
{
    private PlayerController _playerController;
    private PlayerData _playerData;
    private PlayerMoveUtils _playerMoveUtils;
    private PlayerFireUtils _playerFireUtils;
    private BackGround _backGround;
    private BlurCamera _blurCamera;
    public void Enter(PlayerController playerController)
    {
        _playerController = playerController;
        _playerController.CurrentPlayMode = PlayMode.Invincible;
        _playerData = _playerController.GetComponent<PlayerData>();
        _playerMoveUtils = _playerController.GetComponent<PlayerMoveUtils>();
        _playerFireUtils = _playerController.GetComponent<PlayerFireUtils>();

        _backGround = GameObject.FindAnyObjectByType<BackGround>();
        _blurCamera = GameObject.FindAnyObjectByType<BlurCamera>();
        if (!ReferenceEquals(_backGround, null))
        {
            _backGround.ScrollSpeedUp();
        }
        Invincible();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _playerController.PlayerStateContext.ChangeState(_playerController.AutoState);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _playerController.PlayerStateContext.ChangeState(_playerController.ManualState);
        }
        Move();
    }
    public void Exit()
    {
        _backGround.ScrollSpeedDown();
        SetActiveInvincibleVFX(false);
        _blurCamera.SetMotionBlur(false);
        _blurCamera.SetDepthOfField(false);
    }
    public void Move()
    {
        if (_playerMoveUtils.IsPlayerInView())
        {
            Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")).normalized;
            _playerController.transform.Translate(direction * _playerData.Speed * Time.deltaTime);
            _playerMoveUtils.ClampPlayerHorizontalPosition();
            _playerMoveUtils.PlayAnimation(direction);
        }
        else
        {
            _playerMoveUtils.ReversePlayerVerticalPosition();
        }
    }
    private void Invincible()
    {
        SetActiveInvincibleVFX(true);
        _blurCamera.SetMotionBlur(true);
        _blurCamera.SetDepthOfField(true);
    }
    private void SetActiveInvincibleVFX(bool val)
    {
        _playerData.VfxInvincibleModePrefab.SetActive(val);
    }
}
