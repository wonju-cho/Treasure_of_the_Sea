using UnityEngine;

public class MarkTrigger : MonoBehaviour
{
    public GameObject notificationMarkTrigger;

    private bool interactCheck = false;

    //public GameObject QuestUI;
    //private bool once = false;

    private bool questCheck = false;

    public Quest quest;
    private void Awake()
    {
        notificationMarkTrigger.SetActive(false);
        //QuestUI.SetActive(false);
    }

    private void Update()
    {
        if(interactCheck && Input.GetKeyDown(KeyCode.E) && questCheck == false)
        {
            //QuestUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            questCheck = quest.CheckGoals();
            if (questCheck)
            {
                Destroy(notificationMarkTrigger);
            }
            //once = true;
        }

        //if (!interactCheck)
        //    QuestUI.SetActive(false);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && questCheck == false)
        {
            notificationMarkTrigger.SetActive(true);
            interactCheck = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && questCheck == false)
        {
            notificationMarkTrigger.SetActive(false);
            interactCheck = false;
        }
    }
}
