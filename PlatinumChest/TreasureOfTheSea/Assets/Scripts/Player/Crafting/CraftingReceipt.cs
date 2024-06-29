using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemAmount
{
    public InventoryItemData item;

    [Range(1, 99)]
    public int amount;
}

[CreateAssetMenu(menuName = "Crafting System/Crafting Receipt")]
public class CraftingReceipt : ScriptableObject
{
    public List<ItemAmount> materials;
    public InventoryItemData resultItem;
}