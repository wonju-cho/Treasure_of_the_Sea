using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Quest System/Quest Goal")]
public class QuestGoal : ScriptableObject
{
    public string name;
    public bool completed;
    public int requiredAmount;
    public int currentAmount;

    public virtual void Initialize()
    {
        completed = false;
    }

    public void Complete()
    {
        completed = true;
    }
        
}
