using System.Collections;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private float _boomDuration;

    private void Awake()
    {
        _boomDuration = Player.Instance.gameObject.GetComponent<PlayerData>().BoomDuration;
    }

    private void OnEnable()
    {
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(_boomDuration);
        gameObject.SetActive(false);
    }
}
