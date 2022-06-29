using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public GameObject craftingUI;
    public GameObject craftingSignifier;
    public CraftingReceipt craftingRecipt;
    
    public bool isNearTheCrafting = false;
    public bool isCraftingActive = false;

    private InventoryHolder inventoryHolder;
    private InventorySlot_UI[] uiSlots;
    private StaticInventoryDisplay staticInventoryDisplay;
    
    public List<CraftingReceipt> craftingReceipts;

    private int craftingReceiptSize = -1;
    private Bow bow;
    private PlayerManager pm;
    private PlayerController pc;
    private Camera_Controller cm;

    // Start is called before the first frame update
    void Start()
    {
        craftingUI.SetActive(false);
        craftingSignifier.SetActive(false);

        inventoryHolder = GameObject.FindWithTag("Player").GetComponent<InventoryHolder>();
        staticInventoryDisplay = GameObject.FindGameObjectWithTag("InventoryDisplay").GetComponent<StaticInventoryDisplay>();
        uiSlots = staticInventoryDisplay.GetAllSlots();
        craftingReceiptSize = craftingReceipts.Count;
        bow = GameObject.FindWithTag("Bow").GetComponent<Bow>();
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        cm = GameObject.FindWithTag("CameraHolder").GetComponent<Camera_Controller>();


        if (!inventoryHolder)
            Debug.Log("There is no inventory holder in the crafting script");

        if (!staticInventoryDisplay)
            Debug.Log("There is no static inventory display in the crafting script");

        if (uiSlots.Length < 1)
            Debug.Log("UI slots are not initialized in the crafting script");

        if (!bow)
            Debug.Log("There is no bow in the crafting script");

        if (!pm)
            Debug.Log("There is no player manager in the crafting script");

        if (!cm)
            Debug.Log("There is no camera controller in the crafting script");

        if (!pc)
            Debug.Log("There is no player controller in the crafting script");
    }

    // Update is called once per frame
    void Update()
    {
        TriggerCheck();

        if (isCraftingActive)
        {
            CraftingCheck();
        }
    }

    void CraftingCheck()
    {
        if (Input.GetKeyDown(KeyCode.F2)) //make arrow test version
        {
            CraftingReceipt cr = craftingReceipts.Find(i => i.resultItem.displayName == "Arrow");
            CraftTool(cr);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            CraftingReceipt cr = craftingReceipts.Find(i => i.resultItem.displayName == "Plank");
            CraftTool(cr);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            CraftingReceipt cr = craftingReceipts.Find(i => i.resultItem.displayName == "Rope");
            CraftTool(cr);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            CraftingReceipt cr = craftingReceipts.Find(i => i.resultItem.displayName == "Screw");
            CraftTool(cr);
        }
    }


    void CraftTool(CraftingReceipt cr)
    {
        cr.Craft(inventoryHolder);
        UpdateUISlots();        
    }

    void UpdateUISlots()
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            uiSlots[i].UpdateUISlot();
        }
    }

    void TriggerCheck()
    {
        if (isCraftingActive)
        {
            Cursor.visible = true;
            bow.ReMoveCrossHair();
            bow.DisableCrossHair();
            bow.enabled = false;
            pm.enabled = false;
            cm.enabled = false;
            pc.enabled = false;
            craftingUI.SetActive(true);
            craftingSignifier.SetActive(false);
        }
        else
        {
            craftingUI.SetActive(false);
            bow.enabled = true;
            bow.EnableCrossHair();
            pm.enabled = true;
            cm.enabled = true;
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

        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            isCraftingActive = false;
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

    public void ExitCrafting() 
    { 
        isCraftingActive = false; 
    }
}
