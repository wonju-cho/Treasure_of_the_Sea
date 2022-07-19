using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [System.Serializable]
    public class BowSettings
    {
        [Header("Arrow Settings")]
        public int arrowCount;
        public Transform arrowPos;
        public Transform arrowEquipParent;
        public float arrowForce;
        public Rigidbody arrowObject;

        [Header("Bow Equip & UnEquip Settings")]
        public Transform EquipPos;
        public Transform UnEquipPos;
        public Transform UnEquipParent;
        public Transform EquipParent;

        [Header("Bow String Settings")]
        public Transform bowString;
        public Transform stringInitalPos;
        public Transform stringHandPullPos;
        public Transform stringInitialParent;
    }

    [SerializeField]
    public BowSettings bowSettings;

    [Header("Crosshair Settings")]
    public GameObject crossHairObject;

    public GameObject currentCrossHair;

    InventoryHolder inventoryHolder;
    
    Rigidbody currentArrow;

    bool canPullString = false;
    bool canFireArrow = false;

    public int startArrowCount = 10;

    private InventorySlot_UI[] uiSlots;
    private StaticInventoryDisplay staticInventoryDisplay;

    public InventoryItemData arrow;

    private bool canFire = false;

    // Start is called before the first frame update
    void Start()
    {
        inventoryHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryHolder>();
        staticInventoryDisplay = GameObject.FindGameObjectWithTag("InventoryDisplay").GetComponent<StaticInventoryDisplay>();

        uiSlots = staticInventoryDisplay.GetAllSlots();

        inventoryHolder.InventorySystem.AddToInventory(arrow, startArrowCount);

        UpdateUISlots();

        bowSettings.arrowCount = startArrowCount;

        if (!inventoryHolder)
            Debug.Log("There is no inventoryholder in the bow script");

        if (!staticInventoryDisplay)
            Debug.Log("There is no static inventory display in the bow script");

        if (uiSlots.Length < 1)
            Debug.Log("UI slots are not initialized in the bow script");
    }

    // Update is called once per frame
    void Update()
    {
        var arrowSlot = inventoryHolder.InventorySystem.GetInventorySlot("Arrow");
        if(arrowSlot != null)
        {
            bowSettings.arrowCount = arrowSlot.StackSize;
        }
    }

    public void PullString()
    {
        //bowSettings.bowString.transform.position = bowSettings.stringHandPullPos.position;
        //bowSettings.bowString.transform.parent = bowSettings.stringHandPullPos;
    }

    public void ReleaseString()
    {
        //bowSettings.bowString.transform.position = bowSettings.stringInitalPos.position;
        //bowSettings.bowString.transform.parent = bowSettings.stringInitialParent;
    }

    public void PickArrow()
    {
        bowSettings.arrowPos.gameObject.SetActive(true);
        //currentArrow = Instantiate(bowSettings.arrowObject, bowSettings.arrowPos.position, bowSettings.arrowPos.rotation) as GameObject;

    }

    public void DisableArrow()
    {
        bowSettings.arrowPos.gameObject.SetActive(false);
    }

    public void ShowCrosshair(Vector3 crosshairPos)
    {
        if(!currentCrossHair)
        {
            currentCrossHair = Instantiate(crossHairObject) as GameObject;
        }

        currentCrossHair.transform.position = crosshairPos;
        currentCrossHair.transform.LookAt(Camera.main.transform);
    }

    public void ReMoveCrossHair()
    {
        if (currentCrossHair)
            Destroy(currentCrossHair);
    }

    public void UnEquipWeapon()
    {
        this.transform.position = bowSettings.UnEquipPos.position;
        this.transform.rotation = bowSettings.UnEquipPos.rotation;
        this.transform.parent = bowSettings.UnEquipParent;
    }

    public void EquipWeapon()
    {
        this.transform.position = bowSettings.EquipPos.position;
        this.transform.rotation = bowSettings.EquipPos.rotation;
        this.transform.parent = bowSettings.EquipParent;
    }

    public void Fire(Vector3 hitPoint)
    {
        //Debug.Log("fire arrow");

        if(bowSettings.arrowCount >= 1)
        {
            canFire = true;
            Vector3 dir = hitPoint - bowSettings.arrowPos.position;

            currentArrow = Instantiate(bowSettings.arrowObject, bowSettings.arrowPos.position, bowSettings.arrowPos.rotation) as Rigidbody;
            currentArrow.AddForce(dir * bowSettings.arrowForce, ForceMode.VelocityChange);

            bowSettings.arrowCount--;

            var arrowSlot = inventoryHolder.InventorySystem.GetInventorySlot("Arrow");
            arrowSlot.RemoveFromStack(1);

            UpdateUISlots();

        }
        else
        {
            canFire = false;
        }
    }

    void UpdateUISlots()
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            uiSlots[i].UpdateUISlot();
        }
    }

    public bool GetCanFireArrow() { return canFire; }
}
