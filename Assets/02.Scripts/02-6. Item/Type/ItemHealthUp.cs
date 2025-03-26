using UnityEngine;

public class ItemHealthUp : BaseItem
{
    public override void ApplyItem()
    {
        Player.Instance.PlayerData.CurrentHealthPoint += (int)Amount;
        Destroy(gameObject);
    }
}
