using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public GameObject notificationMarkTrigger;

    public GameObject QuestUI;

    private bool questCheck = false;

    public Quest quest;

    bool isInteract = false;

    private void Awake()
    {
        notificationMarkTrigger.SetActive(false);
    }

    private void Update()
    {
        if(isInteract == true && Input.GetKeyDown(KeyCode.E) && questCheck == false)
        {
            Cursor.lockState = CursorLockMode.None;
            questCheck = quest.CheckGoals();
            if (questCheck)
            {
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
