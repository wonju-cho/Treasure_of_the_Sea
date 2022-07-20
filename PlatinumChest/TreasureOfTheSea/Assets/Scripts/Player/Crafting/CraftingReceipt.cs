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
    public InventoryItemData resultItem;

    public bool CanCraft(InventoryHolder inventoryHolder)
    {
        for (int i = 0; i < materials.Count; i++)
        {
            bool check = false;
            check = inventoryHolder.InventorySystem.IsExistSlot(materials[i].item.displayName);
            if (check)
            {
                InventorySlot inventorySlot = inventoryHolder.InventorySystem.GetInventorySlot(materials[i].item.displayName);

                if(inventorySlot.StackSize < materials[i].amount)
                {
                    return false;
                }                
            }
            else
            {
                return false;
            }
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().craftingSFX.Play();
        return true;
    }

    public void Craft(InventoryHolder inventoryHolder)
    {
        if(CanCraft(inventoryHolder))
        {
            if(resultItem.displayName == "Arrow")
            {
                inventoryHolder.InventorySystem.AddToInventory(resultItem, 10);
            }
            else
            {
                inventoryHolder.InventorySystem.AddToInventory(resultItem, 1);
            }
            
            for(int i = 0; i < materials.Count; i++)
            {
                InventorySlot test = inventoryHolder.InventorySystem.GetInventorySlot(materials[i].item.displayName);

                test.RemoveFromStack(materials[i].amount);
            }

        }
    }

    public void Craft(InventoryHolder inventoryHolder, int amount)
    {
        if (CanCraft(inventoryHolder))
        {
            inventoryHolder.InventorySystem.AddToInventory(resultItem, amount);

            for (int i = 0; i < materials.Count; i++)
            {
                InventorySlot test = inventoryHolder.InventorySystem.GetInventorySlot(materials[i].item.displayName);

                //test
                test.RemoveFromStack(materials[i].amount * amount);
            }

        }
    }

}
