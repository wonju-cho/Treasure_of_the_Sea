using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemAmount
{
    public InventoryItemData item;
    
    [Range(1, 999)]
    public int amount;
}

[CreateAssetMenu]
public class CraftingReceipt : ScriptableObject
{
    public List<ItemAmount> materials;
    public List<ItemAmount> results;

    public bool CanCraft()
    {
        return true;
    }

}
