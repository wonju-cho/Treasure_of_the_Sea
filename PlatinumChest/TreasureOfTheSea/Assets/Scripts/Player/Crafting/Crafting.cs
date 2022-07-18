using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crafting : MonoBehaviour
{
    public GameObject craftingSignifier;

    private bool isNearTheCrafting = false;
    private bool isCraftingActive = false;

    UnityEvent craftingEvent;

    // Start is called before the first frame update
    void Start()
    {
        //inventoryHolder = GameObject.FindWithTag("Player").GetComponent<InventoryHolder>();
        //uiSlots = staticInventoryDisplay.GetAllSlots();
        //staticInventoryDisplay = GameObject.FindGameObjectWithTag("InventoryDisplay").GetComponent<StaticInventoryDisplay>();

        if (craftingEvent == null)
            craftingEvent = new UnityEvent();

        //if (!inventoryHolder)
        //    Debug.Log("There is no inventory holder in the crafting script");

        //if (!staticInventoryDisplay)
        //    Debug.Log("There is no static inventory display in the crafting script");

        //if (uiSlots.Length < 1)
        //    Debug.Log("UI slots are not initialized in the crafting script");

        craftingSignifier.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        TriggerCheck();
    }

    //public void CraftingCheck(string receiptName)
    //{
    //    CraftingReceipt cr = craftingReceipts.Find(i => i.resultItem.displayName == receiptName);
    //    CraftTool(cr);
    //}

    //void CraftTool(CraftingReceipt cr)
    //{
    //    cr.Craft(inventoryHolder);
    //    UpdateUISlots();        
    //}

    //void UpdateUISlots()
    //{
    //    for (int i = 0; i < uiSlots.Length; i++)
    //    {
    //        uiSlots[i].UpdateUISlot();
    //    }
    //}

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
            isCraftingActive = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
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

    public bool getCraftingActivated() { return isCraftingActive; }

    public void SetCrafting(bool status) { isCraftingActive = status; }
}
