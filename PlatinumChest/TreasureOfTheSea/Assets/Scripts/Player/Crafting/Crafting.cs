using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public GameObject craftingUI;
    public GameObject craftingSignifier;
    public bool isCraftingActive = false;
    // Start is called before the first frame update
    void Start()
    {
        craftingUI.SetActive(false);
        craftingSignifier.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    { 
        if(isCraftingActive && Input.GetKeyDown(KeyCode.E))
        {
            craftingUI.SetActive(true);
            craftingSignifier.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.BackQuote))
            craftingUI.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isCraftingActive = true;
            craftingSignifier.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isCraftingActive = false;
    }
}
