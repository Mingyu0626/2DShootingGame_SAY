using NUnit.Framework;
using UnityEngine;

public class PlayerMoveUtils : MonoBehaviour
{
    private PlayerData _playerData;
    private Animator _myAnimator;

    private enum AnimatorParam
    {
        X
    }

    private void Awake()
    {
        _playerData = transform.GetComponent<PlayerData>();
        _myAnimator = transform.GetComponent<Animator>();
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

    // �ٸ� Ŭ������ ���°� å�Ӻи��� �´°� �����ѵ�...
    public void PlayAnimation(Vector2 direction)
    {
        // ���� ����(transition)�� �̿��� ��� -> GUI �󿡼� �巡�� & ���
        _myAnimator.SetInteger(nameof(AnimatorParam.X), 
            direction.x < 0 ? -1 : direction.x > 0 ? 1 : 0);
    }
}
