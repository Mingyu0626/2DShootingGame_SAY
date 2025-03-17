using UnityEngine;

public class Boom : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 
            Player.Instance.gameObject.GetComponent<PlayerData>().BoomDuration);
    }
}
