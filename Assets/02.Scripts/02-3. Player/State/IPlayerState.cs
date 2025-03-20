using UnityEngine;

public interface IPlayerState
{
    public void Enter(PlayerController playerController);
    public void Update();
    public void Exit();
}
