using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class Quest : MonoBehaviour
{
    public bool allQuestsCompleted;

    public List<QuestGoal> goals;
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

    private QuestScript_UI questScriptUI;

    public void Start()
    {
        Initialize();
        middle.SetActive(false);
        questUI.SetActive(false);

        fogObject = Instantiate(fogEffect, fogTranslation.position, Quaternion.identity);
    }

    private void Update()
    {
        if (questScriptUI.GetIsQuestUIOn() && !allQuestsCompleted)
        {
            Evaulate();
        }
    }

    private void Initialize()
    {
        allQuestsCompleted = false;

        inventoryHolder = GameObject.FindWithTag("Player").GetComponent<InventoryHolder>();
        staticInventoryDisplay = GameObject.FindWithTag("InventoryDisplay").GetComponent<StaticInventoryDisplay>();
        questScriptUI = GameObject.FindWithTag("QuestScriptUI").GetComponent<QuestScript_UI>();

        UISlots = staticInventoryDisplay.GetAllSlots();

        if (!questScriptUI)
            Debug.Log("There is no quest script UI in the quets trigger.cs");

        if (!inventoryHolder)
            Debug.Log("There is no inventoryHolder in the quest script");

        if (!staticInventoryDisplay)
            Debug.Log("There is no static inventory display in the quest script");

        if (UISlots.Length < 1)
            Debug.Log("UI slots are not initialized in the quest script");

        if (!questScriptUI)
            Debug.Log("There is no quest script UI script in the quets script");

        for (int i = 0; i < goals.Count; i++)
        {
            goals[i].Initialize();
        }

        for (int i = 0; i < goals.Count; i++)
        {
            questSlots[i].SetSprite(goals[i].questItemData.item.icon);
            questSlots[i].SetTMP(goals[i].questItemData.requiredAmount.ToString());
        }
    }

    private void Evaulate()
    {
        bool isInventoryEmpty = true;

        for (int i = 0; i < goals.Count; i++)
        {
            goals[i].called = true;
            var questData = goals[i].questItemData;
            if (inventoryHolder.InventorySystem.IsExistSlot(questData.item.displayName))
            {
                isInventoryEmpty = false;
                InventorySlot slot = inventoryHolder.InventorySystem.GetInventorySlot(questData.item.displayName);
                int currStkSize = slot.StackSize;

                if (currStkSize >= questData.requiredAmount)
                {
                    questSlots[i].EnableCheckImage();
                    goals[i].Complete();
                }
                else
                {
                    questSlots[i].DisableCheckImage();
                }

                if (!questScriptUI.GetIsQuestUIOn())
                {
                    questUI.SetActive(true);
                }
            }
            else
            {
                questSlots[i].DisableCheckImage();
            }
        }

        if (isInventoryEmpty && !questScriptUI.GetIsQuestUIOn())
            questUI.SetActive(true);
    }

    public bool CheckGoals()
    {
        Evaulate();
        allQuestsCompleted = goals.All(g => g.completed);

        if (allQuestsCompleted)
        {
            for (int i = 0; i < goals.Count; i++)
            {
                var questData = goals[i].questItemData;
                InventorySlot slot = inventoryHolder.InventorySystem.GetInventorySlot(questData.item.displayName);
                slot.RemoveFromStack(questData.requiredAmount);
            }

            for (int i = 0; i < UISlots.Length; i++)
            {
                UISlots[i].UpdateUISlot();
            }

            middle.SetActive(true);
            Destroy(questUI);
            Destroy(fogObject);

            for (int i = 0; i < goals.Count; i++)
            {
                if (goals[i].completed)
                {
                    goals[i].Initialize();
                }
            }

            return true;
        }
        return false;
    }

    public void CheatCode()
    {
        middle.SetActive(true);
        Destroy(fogObject);
    }
}
