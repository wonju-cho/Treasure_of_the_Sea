using System.Collections.Generic;
using UnityEngine;

public class QuestScript_UI : MonoBehaviour
{
    //added quest lists from the quest script
    public bool isQuestUIOn;
    public GameObject questScriptUI;
    public List<QuestScripText_UI> questScriptTextUIs;
    public GameObject questParticleSystem;

    public RectTransform particlePosition;
    public GameObject bossBridge;
    private bool check = false;

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
            if (questScriptTextUIs[i].questName == "Chest")
            {
                questScriptTextUIs[i].SetResultQuestText(chestsCount);
            }
            else
            {
                questScriptTextUIs[i].SetResultQuestText(bridgeCount);
            }
        }
        questParticleSystem.GetComponentInChildren<ParticleSystem>().Play();
    }

    public QuestScripText_UI GetQuestScriptTextUI(string questName)
    {
        return questScriptTextUIs.Find(arr => arr != null && arr.questName == questName);
    }

    // Update is called once per frame
    void Update()
    {
        if(bossBridge.GetComponent<Quest>().completed && !check)
        {
            questParticleSystem.SetActive(true);
            questParticleSystem.GetComponentInChildren<ParticleSystem>().Play();
        }

        if (isQuestUIOn)
        {
            questScriptUI.SetActive(true);
            CheckQuestGoals();
            questParticleSystem.SetActive(false);
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
