using UnityEngine;

public class SpeedUpItem : BaseItem
{
    public override void ApplyItem()
    {
        PlayerData.Speed += Amount;
        Destroy(gameObject);
    }
}
