using UnityEngine;

public class ItemFireSpeedUp : BaseItem
{
    public override void ApplyItem()
    {
        PlayerData.CoolTime -= Amount;
        Destroy(gameObject);
    }
}
