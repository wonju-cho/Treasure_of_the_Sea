using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct QuestData
{
    public InventoryItemData item;
    [Range(1, 99)]
    public int requiredAmount;
}

[CreateAssetMenu(menuName = "Quest System/Quest Goal")]
public class QuestGoal : ScriptableObject
{
    public QuestData questItemData;
    public bool completed;

    //for debugging
    public bool called;

    public void Initialize()
    {
        completed = false;
        called = false;
    }

    public void Complete()
    {
        completed = true;
    }
}