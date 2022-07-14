using UnityEngine;
using UnityEngine.UI;

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
