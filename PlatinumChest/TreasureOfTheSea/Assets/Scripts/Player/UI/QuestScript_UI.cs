using System.Collections.Generic;
using UnityEngine;

public class QuestScript_UI : MonoBehaviour
{
    //added quest lists from the quest script
    public bool isQuestUIOn;
    public GameObject questScriptUI;
    public List<QuestScripText_UI> questScriptTextUIs;
    public ParticleSystem PS;

    // Start is called before the first frame update
    void Start()
    {
        isQuestUIOn = false;
        questScriptUI.SetActive(false);

        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");
        int chestsCount = chests.Length;

        GameObject[] bridges = GameObject.FindGameObjectsWithTag("Bridge");
        int bridgeCount = bridges.Length;
        
        for (int i = 0; i < questScriptTextUIs.Count; i++)
        {
            if(questScriptTextUIs[i].questName == "Chest")
            {
                questScriptTextUIs[i].SetResultQuestText(chestsCount);
            }
            else
            {
                questScriptTextUIs[i].SetResultQuestText(bridgeCount);
            }
        }

        if (!PS)
            Debug.Log("There is no ps in the quest script ui script");

        //PS.Simulate(50);
        //PS.Play(true);
    }

    public QuestScripText_UI GetQuestScriptTextUI(string questName)
    {
        return questScriptTextUIs.Find(arr => arr != null && arr.questName == questName);
    }

    // Update is called once per frame
    void Update()
    {
        if (isQuestUIOn)
        {
            questScriptUI.SetActive(true);
            CheckQuestGoals();
            PS.Pause();
        }
        else
        {
            questScriptUI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            isQuestUIOn = isQuestUIOn ? false : true;
        }
    }

    public bool GetIsQuestUIOn() { return isQuestUIOn; }

    void CheckQuestGoals()
    {
        for (int i = 0; i < questScriptTextUIs.Count; i++)
        {
            if(questScriptTextUIs[i].currentInteger >= questScriptTextUIs[i].resultInteger)
            {
                questScriptTextUIs[i].SetRedLineActive(true);
            }
        }
    }

}
