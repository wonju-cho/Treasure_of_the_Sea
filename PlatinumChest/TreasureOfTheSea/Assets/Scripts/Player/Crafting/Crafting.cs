using UnityEngine;
using UnityEngine.Events;

public class Crafting : MonoBehaviour
{
    public GameObject craftingSignifier;
    public AudioSource UI_sfx;


    private bool isNearTheCrafting = false;
    private bool isCraftingActive = false;

    UnityEvent craftingEvent;

    // Start is called before the first frame update
    void Start()
    {
        if (craftingEvent == null)
            craftingEvent = new UnityEvent();

        craftingSignifier.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TriggerCheck();
    }

    void TriggerCheck()
    {
        if (isCraftingActive)
        {
            craftingSignifier.SetActive(false);
        }

        if (isNearTheCrafting && isCraftingActive)
        {
            craftingSignifier.SetActive(false);
        }
        else if (isNearTheCrafting)
        {
            craftingSignifier.SetActive(true);
        }
        else
        {
            craftingSignifier.SetActive(false);
        }

        if (isNearTheCrafting && Input.GetKeyDown(KeyCode.E))
        {
            if (isCraftingActive == false)
            {
                UI_sfx.Play();
            }

            isCraftingActive = true;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isNearTheCrafting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearTheCrafting = false;
        }
    }

    public bool getCraftingActivated() { return isCraftingActive; }

    public void SetCrafting(bool status) { isCraftingActive = status; }

    public void SetIsNearTheCrafting(bool isNear)
    {
        isNearTheCrafting = isNear;
    }
}
