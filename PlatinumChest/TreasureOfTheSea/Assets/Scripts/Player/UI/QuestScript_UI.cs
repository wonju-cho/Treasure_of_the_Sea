using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestScript_UI : MonoBehaviour
{
    public GameObject questScriptUI;
    public bool isQuestUIOn;
    public List<QuestGoal> allQuestLists;
    public RectTransform textPosition;
    public int yOffset;

    // Start is called before the first frame update
    void Start()
    {
        questScriptUI.SetActive(false);
        isQuestUIOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isQuestUIOn)
        {
            questScriptUI.SetActive(true);
            CheckQuestList();
        }
        else
            questScriptUI.SetActive(false);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            isQuestUIOn = isQuestUIOn ? false : true;
        }
    }

    void CheckQuestList()
    {
        //bool once = false;
        for(int i = 0; i < allQuestLists.Count; i++)
        {
            if(allQuestLists[i].called)
            {
                //appear text to the quest UI screen

                allQuestLists[i].TextUI.SetActive(true);
                
                allQuestLists[i].TextUI.GetComponent<RectTransform>().anchoredPosition =
                    new Vector3(textPosition.anchoredPosition.x, textPosition.anchoredPosition.y + (yOffset * i));
            }
        }
    }

}
