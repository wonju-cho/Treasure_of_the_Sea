using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Crafting : MonoBehaviour
{
    public GameObject craftingSignifier;
    public AudioSource uiSfx;

    private bool isNearTheCrafting = false;
    private bool isCraftingActive = false;

    private int arrowAmount = 10;
    private int defaultCraftingAmount = 1;

    public List<CraftingReceipt> receipts;

    // Start is called before the first frame update
    void Start()
    {
        craftingSignifier.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TriggerCheck();
    }

    public bool CanCraft(InventoryHolder inventoryHolder, CraftingReceipt receipt)
    {
        for (int i = 0; i < receipt.materials.Count; i++)
        {
            bool isSlotExist = inventoryHolder.InventorySystem.IsExistSlot(receipt.materials[i].item.displayName);
            if (isSlotExist)
            {
                InventorySlot inventorySlot = inventoryHolder.InventorySystem.GetInventorySlot(receipt.materials[i].item.displayName);

                if (inventorySlot.StackSize < receipt.materials[i].amount)
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

    public void Craft(InventoryHolder inventoryHolder, int idx)
    {
        CraftingReceipt receipt = receipts[idx];
        if (CanCraft(inventoryHolder, receipt))
        {
            if (receipt.resultItem.displayName == "Arrow")
            {
                inventoryHolder.InventorySystem.AddToInventory(receipt.resultItem, arrowAmount);
            }
            else
            {
                inventoryHolder.InventorySystem.AddToInventory(receipt.resultItem, defaultCraftingAmount);
            }

            for (int i = 0; i < receipt.materials.Count; i++)
            {
                InventorySlot slot = inventoryHolder.InventorySystem.GetInventorySlot(receipt.materials[i].item.displayName);
                slot.RemoveFromStack(receipt.materials[i].amount);
            }
        }
    }

    public void Craft(InventoryHolder inventoryHolder, int idx, int amount)
    {
        CraftingReceipt receipt = receipts[idx];
        if (CanCraft(inventoryHolder, receipt))
        {
            inventoryHolder.InventorySystem.AddToInventory(receipt.resultItem, amount);

            for (int i = 0; i < receipt.materials.Count; i++)
            {
                InventorySlot slot = inventoryHolder.InventorySystem.GetInventorySlot(receipt.materials[i].item.displayName);
                slot.RemoveFromStack(receipt.materials[i].amount * amount);
            }
        }
    }

    void TriggerCheck()
    {
        if (isCraftingActive)
        {
            craftingSignifier.SetActive(false);
        }

        if (isNearTheCrafting && isCraftingActive)
        {
            craftingSignifier.SetActive(false);
        }
        else if (isNearTheCrafting)
        {
            craftingSignifier.SetActive(true);
        }
        else
        {
            craftingSignifier.SetActive(false);
        }

        if (isNearTheCrafting && Input.GetKeyDown(KeyCode.E))
        {
            if (isCraftingActive == false)
            {
                uiSfx.Play();
            }

            isCraftingActive = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearTheCrafting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearTheCrafting = false;
        }
    }

    public bool GetCraftingActivated() { return isCraftingActive; }

    public void SetCrafting(bool status) { isCraftingActive = status; }

    public void SetIsNearTheCrafting(bool isNear)
    {
        isNearTheCrafting = isNear;
    }
}
