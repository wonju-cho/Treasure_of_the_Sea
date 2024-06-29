using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    private QuestScript_UI questScriptUI;
    private Quest quest;
    public GameObject notificationMarkTrigger;
    public GameObject QuestUI;

    private bool questCompleted = false;
    bool isInteract = false;

    private void Awake()
    {
        notificationMarkTrigger.SetActive(false);

        Transform parentTransform = transform.parent;
        quest = parentTransform.GetComponent<Quest>();
        questScriptUI = GameObject.FindGameObjectWithTag("QuestScriptUI").GetComponent<QuestScript_UI>();

        if (quest == null)
        {
            Debug.LogWarning($"Quest cs is not assigned to {this.gameObject}");
        }
    }

    private void Update()
    {
        if (isInteract == true && Input.GetKeyDown(KeyCode.E) && questCompleted == false)
        {
            questCompleted = quest.CheckGoals();
            if (questCompleted)
            {
                var scriptText = questScriptUI.GetQuestScriptTextUI("Bridge");
                int num = scriptText.GetCurrentQuestText();
                num += 1;
                scriptText.SetCurrentQuestText(num);

                Destroy(notificationMarkTrigger);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && questCompleted == false)
        {
            notificationMarkTrigger.SetActive(true);
            isInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && questCompleted == false)
        {
            QuestUI.SetActive(false);
            notificationMarkTrigger.SetActive(false);
            isInteract = false;
        }
    }

    public void SetQuestCheck(bool isDone)
    {
        questCompleted = isDone;

        if (questCompleted)
            Destroy(notificationMarkTrigger);
    }

    public void CheatCode()
    {
        questCompleted = true;
        this.enabled = false;
    }
}
