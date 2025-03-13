using NUnit.Framework;
using UnityEngine;

public class PlayerMoveUtils : MonoBehaviour
{
    private PlayerData _playerData;
    
    private void Awake()
    {
        _playerData = transform.GetComponent<PlayerData>();
    }
    public bool IsPlayerInView()
    {
        return Mathf.Abs(transform.position.x) <= _playerData.VerticalLimit;
    }
    public void ClampPlayerHorizontalPosition()
    {
        transform.position =
                new Vector2(transform.position.x,
                    Mathf.Clamp
                    (transform.position.y, 
                    -_playerData.HorizontalLimit, _playerData.HorizontalLimit));
    }
    public void ReversePlayerVerticalPosition()
    {
        transform.position =
               new Vector2(
                   Mathf.Clamp(-transform.position.x, -_playerData.VerticalLimit, _playerData.VerticalLimit),
                   Mathf.Clamp(transform.position.y, -_playerData.HorizontalLimit, _playerData.HorizontalLimit));
    }
    public void SpeedCheck()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _playerData.Speed += _playerData.SpeedVariance;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _playerData.Speed -= _playerData.SpeedVariance;
        }
    }
}
