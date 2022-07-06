using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;

    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;

    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    public abstract void AssignSlot(InventorySystem invToDisplay);

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach (var slot in SlotDictionary)
        {
            if(slot.Value == updatedSlot) // Slot value - the "under the hood" inventory slot.
            {
                slot.Key.UpdateUISlot(updatedSlot); //Slot key - the UI representation of the value.
            }
        }
    }

    public void SlotClicked(InventorySlot_UI clickedSlot)
    {
        Debug.Log("Slot clicked");
    }
}
