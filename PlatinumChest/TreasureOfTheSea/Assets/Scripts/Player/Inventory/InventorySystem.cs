using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;


    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++) 
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        if(ContainsItem(itemToAdd, out List<InventorySlot> inventorySlot)) //check whether item exitsts in inventory.
        {
            foreach (var slot in inventorySlot)
            {
                if(slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
            
        }
        
        if(HasFreeSlot(out InventorySlot freeSlot)) // Gets the first available slot
        {
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }
        
        return false;
    }

    //public void RemoveFromInventory(InventorySlot slot, int amountToRemove)
    //{
    //    slot.RemoveFromStack(amountToRemove);
    //    OnInventorySlotChanged?.Invoke(slot);
    //}

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> inventorySlot)
    {
        inventorySlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();
        //InventorySlots.First(slots => slots.ItemData.maxStackSize > 5);

        return inventorySlot == null ? false : true;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null); //has it found an empty slot?
        return freeSlot == null ? false : true;
    }
    
    public InventorySlot GetInventorySlot(string name)
    {
        InventorySlot findSlot = InventorySlots.Find(i => i.ItemData.name == name);
        return findSlot;
    }

    public bool IsExistSlot(string name)
    {
        InventorySlot findSlot = InventorySlots.Find(i => i.ItemData != null && i.ItemData.name == name);

        if (findSlot == null)
            return false;
        else
            return true;
    }

}
