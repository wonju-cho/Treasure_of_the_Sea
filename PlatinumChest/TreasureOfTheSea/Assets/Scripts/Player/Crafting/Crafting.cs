using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    private InventoryHolder inventoryHolder;
    private InventorySlot_UI[] uiSlots;
    private StaticInventoryDisplay staticInventoryDisplay;

    public GameObject craftingUI;
    public GameObject craftingSignifier;
    public List<CraftingReceipt> craftingReceipts;
    private int craftingReceiptSize = -1;
    private bool isNearTheCrafting = false;
    private bool isCraftingActive = false;
    //private FollowWorld playerHotbar;
    private GameObject playerHotbar;
    public GameObject inventoryPosition;

    private Bow bow;
    private PlayerManager pm;
    private PlayerController pc;
    private Camera_Controller cm;


    // Start is called before the first frame update
    void Start()
    {
        craftingUI.SetActive(false);
        craftingSignifier.SetActive(false);

        //playerHotbar = GameObject.FindWithTag("InventoryDisplay").GetComponent<FollowWorld>();
        playerHotbar = GameObject.FindWithTag("InventoryDisplay");
        inventoryHolder = GameObject.FindWithTag("Player").GetComponent<InventoryHolder>();
        staticInventoryDisplay = GameObject.FindGameObjectWithTag("InventoryDisplay").GetComponent<StaticInventoryDisplay>();
        bow = GameObject.FindWithTag("Bow").GetComponent<Bow>();
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        cm = GameObject.FindWithTag("CameraHolder").GetComponent<Camera_Controller>();
        
        uiSlots = staticInventoryDisplay.GetAllSlots();
        craftingReceiptSize = craftingReceipts.Count;

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

        if (!playerHotbar)
            Debug.Log("There is no follow world of inventory hot bar in the crafting slot_ui script");

    }

    // Update is called once per frame
    void Update()
    {
        TriggerCheck();
    }


    public void CraftingCheck(string receiptName)
    {
        CraftingReceipt cr = craftingReceipts.Find(i => i.resultItem.displayName == receiptName);
        CraftTool(cr);
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
            Cursor.lockState = CursorLockMode.None;
            craftingUI.SetActive(true);
            craftingSignifier.SetActive(false);
            pc.enabled = false;
            pm.enabled = false;
            bow.enabled = false;
            //playerHotbar.enabled = false;

            if(!playerHotbar.active)
            {
                playerHotbar.SetActive(true);
                playerHotbar.GetComponent<FollowWorld>().enabled = false;
            }

            playerHotbar.GetComponent<RectTransform>().anchoredPosition = inventoryPosition.GetComponent<RectTransform>().anchoredPosition;
            //playerHotbar.transform.position = inventoryPosition.transform.position;
        }
        else
        {
            craftingUI.SetActive(false);
            pc.enabled = true;
            pm.enabled = true;
            bow.enabled = true;
            playerHotbar.GetComponent<FollowWorld>().enabled = true;
            //playerHotbar.enabled = true;
            //playerHotbar.SetActive(true);
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

    public void ExitCrafting() 
    {
        isCraftingActive = false; 
    }

    public bool getCraftingActivated() { return isCraftingActive; }
}
