using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item
{
    protected ItemType _itemType;
    public ItemType itemType
    {
        get { return _itemType; }
    }

}


public enum ItemType
{
    FRUIT,
    BRANCH,

    STONE,

    CLOTHES
}