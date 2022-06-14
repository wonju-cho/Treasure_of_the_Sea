using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Quest : MonoBehaviour
{    
    public InventoryHolder inventoryHolder;

    //public QuestCompletedEvent questCompleted;

    public bool completed;

    public List<QuestGoal> Goals;

    public GameObject middle;

    public List<InventorySlot_UI> UISlots;


    public void Start()
    {
        Initialize();
        middle.SetActive(false);
    }

    public void Initialize()
    {
        completed = false;
        for (int i = 0; i < Goals.Count; i++)
        {
            Goals[i].Initialize();
        }
        //questCompleted = new QuestCompletedEvent();
    }
    protected void Evaulate()
    {
        Debug.Log("Enter in the evaulate function");
        for(int i = 0; i < Goals.Count; i++)
        {
            if (inventoryHolder.InventorySystem.IsExistSlot(Goals[i].requiredName))
            {
                InventorySlot test = inventoryHolder.InventorySystem.GetInventorySlot(Goals[i].requiredName);
                Goals[i].currentAmount = test.StackSize;
                
                int check = Goals[i].currentAmount;
                if (check >= Goals[i].requiredAmount)
                {
                    Goals[i].Complete();
                }
            }
        }
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
            return true;
        }
        return false;
    }
}

public class QuestCompletedEvent : UnityEvent<Quest> { }
