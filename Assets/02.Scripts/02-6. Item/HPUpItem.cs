using UnityEngine;

public class HPUpItem : BaseItem
{
    public override void ApplyItem()
    {
        Player.Instance.CurrentHealthPoint += (int)Amount;
        Destroy(gameObject);
    }
}
