using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class Quest : MonoBehaviour
{    
    public InventoryHolder inventoryHolder;

    //public QuestCompletedEvent questCompleted;

    public bool completed;

    public List<QuestGoal> Goals;

    public GameObject middle;

    public List<InventorySlot_UI> UISlots;
    
    public List<QuestSlot_UI> questSlots;

    public GameObject questUI;

    public void Start()
    {
        Initialize();
        middle.SetActive(false);
        questUI.SetActive(false);
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
        //questCompleted = new QuestCompletedEvent();
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
                    questUI.SetActive(true);
                }
            }
        }
        if(isEmptyInventory)
            questUI.SetActive(true);
    }

    public bool CheckGoals()
    {
        Debug.Log("Enter in the Check Goals function");
        Evaulate();
        completed = Goals.All(g => g.completed);
        if(completed)
        {
            Debug.Log("Complte quest");
            //questCompleted.Invoke(this);
            //questCompleted.RemoveAllListeners();

            for(int i = 0; i < Goals.Count; i++)
            {
                InventorySlot test = inventoryHolder.InventorySystem.GetInventorySlot(Goals[i].requiredName);
                test.RemoveFromStack(Goals[i].requiredAmount);
            }

            for (int i = 0; i < UISlots.Count; i++)
            {
                UISlots[i].UpdateUISlot();                
            }

            middle.SetActive(true);
            Destroy(questUI);
            return true;
        }
        return false;
    }
}

public class QuestCompletedEvent : UnityEvent<Quest> { }
