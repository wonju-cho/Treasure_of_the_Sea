using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitItem : Item
{
    private Transform ItemModel;
    
    public FruitItem(Transform transform)
    {
        ItemModel = transform;
        _itemType = ItemType.FRUIT;
    }
}
