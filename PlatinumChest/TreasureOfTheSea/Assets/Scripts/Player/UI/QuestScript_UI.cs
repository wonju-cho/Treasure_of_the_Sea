using System.Collections.Generic;
using UnityEngine;

public class QuestScript_UI : MonoBehaviour
{
    //added quest lists from the quest script
    public bool isQuestScriptUIOn;
    public GameObject questScriptUI;
    public List<QuestScripText_UI> questScriptTextUIs;
    public GameObject questParticleSystem;

    public RectTransform particlePosition;
    public GameObject bossBridge;
    private bool check = false;

    public GameObject skullQuestUI;

    // Start is called before the first frame update
    void Start()
    {
        isQuestScriptUIOn = false;
        questScriptUI.SetActive(false);

        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");
        int chestsCount = chests.Length;

        GameObject[] bridges = GameObject.FindGameObjectsWithTag("Bridge");
        int bridgeCount = bridges.Length;

        int skullsCount = chestsCount;

        for (int i = 0; i < questScriptTextUIs.Count; i++)
        {
            if (questScriptTextUIs[i].questName == "Chest")
            {
                questScriptTextUIs[i].SetResultQuestText(chestsCount);
            }
            else if (questScriptTextUIs[i].questName == "Skull")
            {
                questScriptTextUIs[i].SetResultQuestText(skullsCount);
            }
            else
            {
                questScriptTextUIs[i].SetResultQuestText(bridgeCount);
            }
        }

        skullQuestUI.SetActive(false);

        questParticleSystem.GetComponentInChildren<ParticleSystem>().Play();
    }

    public QuestScripText_UI GetQuestScriptTextUI(string questName)
    {
        return questScriptTextUIs.Find(arr => arr != null && arr.questName == questName);
    }

    // Update is called once per frame
    void Update()
    {
        if (bossBridge.GetComponent<Quest>().allQuestsCompleted && !check)
        {
            questParticleSystem.SetActive(true);
            questParticleSystem.GetComponentInChildren<ParticleSystem>().Play();
        }

        if (isQuestScriptUIOn)
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
            isQuestScriptUIOn = isQuestScriptUIOn ? false : true;
        }
    }

    public bool GetIsQuestUIOn() { return isQuestScriptUIOn; }

    void CheckQuestGoals()
    {
        for (int i = 0; i < questScriptTextUIs.Count; i++)
        {
            if (questScriptTextUIs[i].currentInteger >= questScriptTextUIs[i].resultInteger)
            {
                questScriptTextUIs[i].SetRedLineActive(true);
                if (questScriptTextUIs[i].questName == "Chest")
                    skullQuestUI.SetActive(true);
            }
        }
    }

}
