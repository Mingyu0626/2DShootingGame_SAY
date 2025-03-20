using System.Collections.Generic;
using UnityEngine;




public class ItemData : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _itemList;
    public List<GameObject> ItemList
    {
        get => _itemList;
    }
}
