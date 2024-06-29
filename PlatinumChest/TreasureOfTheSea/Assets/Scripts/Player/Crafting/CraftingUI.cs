using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    public GameObject[] allRenderers;
    private GameObject[] craftingUIs;
    public Button exitButton;

    private GameObject playerHotbar;
    private InventoryHolder inventoryHolder;
    private InventorySlot_UI[] uiSlots;
    private StaticInventoryDisplay staticInventoryDisplay;

    public GameObject inventoryPosition;
    public Texture2D cursorTexture;

    private Bow bow;
    private PlayerManager pm;
    private PlayerController pc;

    bool craftingCheck = false;
    public PauseMenu pauseUI;
    public TreasureBox tb;
    public AudioSource UI_sfx;

    //test
    public Crafting crafting;

    bool sfx_play = false;

    // Start is called before the first frame update
    void Start()
    {
        inventoryHolder = GameObject.FindWithTag("Player").GetComponent<InventoryHolder>();
        staticInventoryDisplay = GameObject.FindGameObjectWithTag("InventoryDisplay").GetComponent<StaticInventoryDisplay>();
        playerHotbar = GameObject.FindWithTag("InventoryDisplay");

        crafting = GameObject.FindGameObjectWithTag("Crafting").GetComponent<Crafting>();
        craftingUIs = GameObject.FindGameObjectsWithTag("Crafting");
        bow = GameObject.FindWithTag("Bow").GetComponent<Bow>();
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();

        exitButton.GetComponent<Button>();
        exitButton.onClick.AddListener(CloseCraftingUI);

        uiSlots = staticInventoryDisplay.GetAllSlots();

        CloseCraftingUI();
        sfx_play = true;

        if (craftingUIs.Length < 1)
        {
            Debug.Log("there is no crafting");
        }

        if (!crafting)
        {
            Debug.LogWarning($"Crafting cs is not assigned to {this.gameObject}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CraftingActiveCheck();

        if (craftingCheck)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);

            pc.enabled = false;
            pm.enabled = false;
            bow.enabled = false;

            for (int i = 0; i < allRenderers.Length; i++)
            {
                allRenderers[i].SetActive(true);
            }

            if (!playerHotbar.activeSelf)
            {
                playerHotbar.SetActive(true);
            }

            playerHotbar.GetComponent<FollowWorld>().enabled = false;
            playerHotbar.GetComponent<RectTransform>().anchoredPosition = inventoryPosition.GetComponent<RectTransform>().anchoredPosition;
        }
        else if (!craftingCheck && !pauseUI.isGamePasued && !tb.gameEnd)
        {
            Cursor.visible = false;
        }
    }

    public void CloseCraftingUI()
    {
        Cursor.visible = false;
        playerHotbar.GetComponent<FollowWorld>().enabled = true;
        pc.enabled = true;
        pm.enabled = true;
        bow.enabled = true;

        for (int i = 0; i < allRenderers.Length; i++)
        {
            allRenderers[i].SetActive(false);
        }

        for (int i = 0; i < craftingUIs.Length; i++)
        {
            craftingUIs[i].GetComponent<Crafting>().SetCrafting(false);
        }

        craftingCheck = false;

        if (sfx_play)
        {
            UI_sfx.Play();
        }
    }

    void CraftingActiveCheck()
    {
        for (int i = 0; i < craftingUIs.Length; i++)
        {
            if (craftingUIs[i].GetComponent<Crafting>().GetCraftingActivated() == true)
            {
                craftingCheck = true;
                break;
            }
        }
    }

    public bool GetCraftingUIActive() { return craftingCheck; }

    public void CraftTool(string receiptName)
    {
        int idx = crafting.receipts.FindIndex(i => i.resultItem.displayName == receiptName);

        if (idx != -1)
        {
            crafting.Craft(inventoryHolder, idx);
            UpdateUISlots();
        }
    }

    void UpdateUISlots()
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            uiSlots[i].UpdateUISlot();
        }
    }
}
