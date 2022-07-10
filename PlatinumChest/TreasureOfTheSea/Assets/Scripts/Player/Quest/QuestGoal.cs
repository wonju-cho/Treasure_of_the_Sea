using UnityEngine;
using TMPro;
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

    [TextArea(2, 3)]
    public string questDescription;
    //public GameObject TextUI;
    //private Image redLineImage;

    public virtual void Initialize()
    {
        completed = false;
        called = false;
        once = false;
        currentAmount = 0;
        //TextUI.GetComponent<TextMeshProUGUI>().text = questDescription;
        //redLineImage = TextUI.GetComponentInChildren<Image>();
        //redLineImage.enabled = false;
    }

    public void Complete()
    {
        completed = true;
        //redLineImage.enabled = true;
    }

        
}
