using UnityEngine;

public class ItemMoveSpeedUp : BaseItem
{
    public override void ApplyItem()
    {
        PlayerData.Speed += Amount;
        Destroy(gameObject);
    }
}
