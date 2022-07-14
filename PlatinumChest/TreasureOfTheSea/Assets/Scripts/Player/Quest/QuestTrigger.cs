using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public GameObject notificationMarkTrigger;

    public GameObject QuestUI;

    private bool questCheck = false;

    public Quest quest;

    bool isInteract = false; 
    private QuestScript_UI questScriptUI;


    private void Awake()
    {
        notificationMarkTrigger.SetActive(false);
        questScriptUI = GameObject.FindGameObjectWithTag("QuestScriptUI").GetComponent<QuestScript_UI>();

    }

    private void Update()
    {
        if(isInteract == true && Input.GetKeyDown(KeyCode.E) && questCheck == false)
        {
            Cursor.lockState = CursorLockMode.None;
            questCheck = quest.CheckGoals();
            if (questCheck)
            {
                var scriptText = questScriptUI.GetQuestScriptTextUI("Bridge");
                int num = scriptText.GetCurrentQuetsText();
                num += 1;
                scriptText.SetCurrentQuestText(num);

                Destroy(notificationMarkTrigger);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && questCheck == false)
        {
            notificationMarkTrigger.SetActive(true);
            isInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && questCheck == false)
        {
            QuestUI.SetActive(false);
            notificationMarkTrigger.SetActive(false);
            isInteract = false;
        }
    }
}
