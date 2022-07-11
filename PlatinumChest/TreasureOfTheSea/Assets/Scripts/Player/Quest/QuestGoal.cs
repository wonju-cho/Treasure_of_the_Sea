using UnityEngine;

[CreateAssetMenu(menuName = "Quest System/Quest Goal")]
public class QuestGoal : ScriptableObject
{
    public string requiredName;
    public bool completed;
    public bool called;
    public int requiredAmount;
    public int currentAmount;
    public Sprite icon;
    public bool once;

    [TextArea(2, 3)]
    public string questDescription;

    public virtual void Initialize()
    {
        completed = false;
        called = false;
        once = false;
        currentAmount = 0;
    }

    public void Complete()
    {
        completed = true;
    }   
}
