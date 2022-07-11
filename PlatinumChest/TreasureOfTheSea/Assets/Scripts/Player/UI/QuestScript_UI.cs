using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestScript_UI : MonoBehaviour
{
    public GameObject questScriptUI;
    public GameObject questScriptTextPrefab;
    public bool isQuestUIOn;
    private GameObject[] questScriptPrefabs;
    public List<QuestGoal> allQuestLists;
    public RectTransform textPosition;
    public int yOffset;

    // Start is called before the first frame update
    void Start()
    {
        questScriptUI.SetActive(false);
        isQuestUIOn = false;
        questScriptPrefabs = new GameObject[allQuestLists.Count];
    }

    // Update is called once per frame
    void Update()
    {
        if (isQuestUIOn)
        {
            questScriptUI.SetActive(true);
            CheckQuestList();
            SetScriptActive(true);
        }
        else
        {
            questScriptUI.SetActive(false);
            SetScriptActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            isQuestUIOn = isQuestUIOn ? false : true;
        }
    }

    public bool GetIsQuestUIOn() { return isQuestUIOn; }

    void SetScriptActive(bool value)
    {
        if (questScriptPrefabs[0] != null)
        {
            for (int i = 0; i < questScriptPrefabs.Length; i++)
            {
                questScriptPrefabs[i].SetActive(value);
            }
        }
    }

    void CheckQuestList()
    {
        for(int i = 0; i < allQuestLists.Count; i++)
        {
            if(allQuestLists[i].called)
            {
                if(!allQuestLists[i].once)
                {
                    var ob = Instantiate(questScriptTextPrefab, new Vector3(textPosition.localPosition.x, textPosition.localPosition.y + (yOffset * i)), Quaternion.identity, transform.parent);
                    ob.GetComponent<RectTransform>().anchoredPosition = new Vector3(textPosition.anchoredPosition.x, textPosition.anchoredPosition.y + (yOffset * i));
                    questScriptTextPrefab.GetComponent<TextMeshProUGUI>().text = allQuestLists[i].questDescription;
                    questScriptTextPrefab.GetComponentInChildren<Image>().enabled = false;
                    questScriptPrefabs.SetValue(ob, i);
                    allQuestLists[i].once = true;
                }
            }
        }
    }

    public GameObject[] GetAllQuestScripts() { return questScriptPrefabs; }
}
