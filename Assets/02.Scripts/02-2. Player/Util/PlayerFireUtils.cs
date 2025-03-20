using System.Collections;
using UnityEngine;



public class PlayerFireUtils : MonoBehaviour
{
    private PlayerData _playerData;


    private void Awake()
    {
        _playerData = transform.GetComponent<PlayerData>();
    }
    public IEnumerator CoolDown()
    {
        _playerData.IsCoolDown = true;
        yield return new WaitForSeconds(_playerData.CoolTime);
        _playerData.IsCoolDown = false;
    }
}
