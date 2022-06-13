using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Quest : MonoBehaviour
{    
    public InventoryHolder inventoryHolder;

    public QuestCompletedEvent questCompleted;

    public bool completed;

    public List<QuestGoal> Goals;

    public GameObject middle;
    public void Start()
    {
        Initialize();
        middle.SetActive(false);
    }

    public void Initialize()
    {
        completed = false;
        questCompleted = new QuestCompletedEvent();
    }
    protected void Evaulate()
    {
        Debug.Log("Enter in the evaulate function");
        for(int i = 0; i < Goals.Count; i++)
        {
            InventorySlot test = inventoryHolder.InventorySystem.GetInventorySlot(Goals[i].name);
            if (test != null)
            {
                Goals[i].currentAmount = test.StackSize;
                int check = Goals[i].currentAmount;
                if (check >= Goals[i].requiredAmount)
                {
                    Goals[i].Complete();
                }
            }
        }
    }

    public void CheckGoals()
    {
        Debug.Log("Enter in the Check Goals function");
        Evaulate();
        completed = Goals.All(g => g.completed);
        if(completed)
        {
            Debug.Log("Complte quest");
            questCompleted.Invoke(this);
            questCompleted.RemoveAllListeners();
            middle.SetActive(true);
        }
    }
}

public class QuestCompletedEvent : UnityEvent<Quest> { }
