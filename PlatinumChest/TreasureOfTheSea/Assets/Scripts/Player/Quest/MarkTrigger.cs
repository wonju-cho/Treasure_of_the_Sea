using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MarkTrigger : MonoBehaviour
{
    public GameObject notificationMarkTrigger;

    private bool interactCheck = false;

    //public GameObject QuestUI;
    private bool once = false;

    public Quest quest;
    private void Awake()
    {
        notificationMarkTrigger.SetActive(false);
        //QuestUI.SetActive(false);
    }

    private void Update()
    {
        if(interactCheck && Input.GetKeyDown(KeyCode.E) && once == false)
        {
            //QuestUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            quest.CheckGoals();
            Destroy(notificationMarkTrigger);
            once = true;
        }

        //if (!interactCheck)
        //    QuestUI.SetActive(false);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && once == false)
        {
            notificationMarkTrigger.SetActive(true);
            interactCheck = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && once == false)
        {
            notificationMarkTrigger.SetActive(false);
            interactCheck = false;
        }
    }
}
