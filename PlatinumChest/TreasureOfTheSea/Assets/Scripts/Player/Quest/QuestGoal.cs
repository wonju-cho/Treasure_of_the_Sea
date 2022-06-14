using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Quest System/Quest Goal")]
public class QuestGoal : ScriptableObject
{
    //public struct Info
    //{
    //    public string requiredName;
    //    public string requireAmount;
    //    public bool completed;
    //    public int currentAmount;
    //}

    //public List<Info> questGoals;
    public string requiredName;
    public bool completed;
    public int requiredAmount;
    public int currentAmount;

    public virtual void Initialize()
    {
        completed = false;
        currentAmount = 0;
        //for(int i = 0; i < questGoals.Count; i++)
        //{
        //    //questGoals[i].completed = false;
        //    questGoals[i].completed = false;
        //}
    }

    public void Complete()
    {
        completed = true;
    }
        
}
