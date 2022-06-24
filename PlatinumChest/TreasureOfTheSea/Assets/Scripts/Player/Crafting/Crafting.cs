using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public GameObject craftingUI;
    public bool isCraftingActive = false;
    // Start is called before the first frame update
    void Start()
    {
        craftingUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    { 
        if(isCraftingActive && Input.GetKeyDown(KeyCode.E))
        {
            craftingUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Escape))
                craftingUI.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            isCraftingActive = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isCraftingActive = false;
    }
}
