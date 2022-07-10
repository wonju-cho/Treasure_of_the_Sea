using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Quest : MonoBehaviour
{    
    public bool completed;

    public List<QuestGoal> Goals;
    public List<QuestSlot_UI> questSlots;
    public GameObject middle;
    public GameObject questUI;

    [Header("Fog Settings For Bridge")]
    public GameObject fogEffect;
    public Transform fogTranslation;

    private GameObject fogObject;

    private InventoryHolder inventoryHolder;
    private StaticInventoryDisplay staticInventoryDisplay;
    private InventorySlot_UI[] UISlots;

    public void Start()
    {
        Initialize();
        middle.SetActive(false);
        questUI.SetActive(false);

        fogObject = Instantiate(fogEffect, fogTranslation.position, Quaternion.identity);

        inventoryHolder = GameObject.FindWithTag("Player").GetComponent<InventoryHolder>();
        staticInventoryDisplay = GameObject.FindWithTag("InventoryDisplay").GetComponent<StaticInventoryDisplay>();
        UISlots = staticInventoryDisplay.GetAllSlots();

        if (!inventoryHolder)
            Debug.Log("There is no inventoryHolder in the quest script");

        if (!staticInventoryDisplay)
            Debug.Log("There is no static inventory display in the quest script");

        if (UISlots.Length < 1)
            Debug.Log("UI slots are not initialized in the quest script");

    }

    public void Initialize()
    {
        completed = false;
        for (int i = 0; i < Goals.Count; i++)
        {
            Goals[i].Initialize();
        }

        for(int i = 0; i < Goals.Count; i++)
        {
            questSlots[i].SetSprite(Goals[i].icon);
            questSlots[i].SetTMP(Goals[i].requiredAmount.ToString());
        }
    }

    protected void Evaulate()
    {
        Debug.Log("Enter in the evaulate function");
        bool isEmptyInventory = true;
        for(int i = 0; i < Goals.Count; i++)
        {
            if (inventoryHolder.InventorySystem.IsExistSlot(Goals[i].requiredName))
            {
                isEmptyInventory = false;
                InventorySlot test = inventoryHolder.InventorySystem.GetInventorySlot(Goals[i].requiredName);
                Goals[i].currentAmount = test.StackSize;
                
                int check = Goals[i].currentAmount;

                if (check >= Goals[i].requiredAmount)
                {
                    questSlots[i].EnableCheckImage();
                    Goals[i].Complete();
                }
                questUI.SetActive(true);
            }
        }

        if(isEmptyInventory)
            questUI.SetActive(true);
    }


    public bool CheckGoals()
    {
        Evaulate();
        completed = Goals.All(g => g.completed);
        if(completed)
        {

            for(int i = 0; i < Goals.Count; i++)
            {
                InventorySlot test = inventoryHolder.InventorySystem.GetInventorySlot(Goals[i].requiredName);
                test.RemoveFromStack(Goals[i].requiredAmount);
            }

            for (int i = 0; i < UISlots.Length; i++)
            {
                UISlots[i].UpdateUISlot();                
            }

            middle.SetActive(true);
            Destroy(questUI);
            Destroy(fogObject);

            return true;
        }
        return false;
    }
}
