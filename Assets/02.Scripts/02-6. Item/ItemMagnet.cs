using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : BaseItem
{
    public override void ApplyItem()
    {
        List<GameObject> allItemObjects 
            = new List<GameObject>(GameObject.FindGameObjectsWithTag("Item"));

        BaseItem baseItem;
        foreach (GameObject item in allItemObjects)
        {
            baseItem = item.GetComponent<BaseItem>();
            if (!ReferenceEquals(baseItem, null))
            {
                baseItem.GoToPlayer();
            }
        }
    }
}
