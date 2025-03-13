using UnityEngine;

public class FireSpeedUpItem : BaseItem
{
    public override void ApplyItem()
    {
        PlayerData.CoolTime -= Amount;
        Destroy(gameObject);
    }
}
