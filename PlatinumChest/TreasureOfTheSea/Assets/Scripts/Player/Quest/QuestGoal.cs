using UnityEngine;

[CreateAssetMenu(menuName = "Quest System/Quest Goal")]
public class QuestGoal : ScriptableObject
{
    public string requiredName;
    public bool completed;
    public int requiredAmount;
    public int currentAmount;
    public Sprite icon;

    public virtual void Initialize()
    {
        completed = false;
        currentAmount = 0;
    }

    public void Complete()
    {
        completed = true;
    }
        
}
