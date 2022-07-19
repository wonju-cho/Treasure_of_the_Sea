using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditUI : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject creditUI;
    public Texture2D cursorTexture;
    private Bow bow;
    private PlayerManager pm;
    private PlayerController pc;
    public bool isCreditOn = false;
    public bool isMainGameOn = false;
    public GameObject otherUI;
    public GameObject treasureBoxUI;
    public TreasureBox tb;

    public int currentIndex;
    public List<GameObject> creditTexts;

    private void Start()
    {
        creditUI.SetActive(false);
        currentIndex = 0;

        if(SceneManager.GetActiveScene().name == "MainScene")
        {
            isMainGameOn = true;
            bow = GameObject.FindWithTag("Bow").GetComponent<Bow>();
            pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            pm = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        }
    }

    public void OpenCreditUI()
    {
        audioSource.Play();
        currentIndex = 0;
        UpdateScene(currentIndex);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);

        if(isMainGameOn)
        {
            pc.enabled = false;
            pm.enabled = false;
            bow.enabled = false;
            otherUI.SetActive(false);
            treasureBoxUI.SetActive(false);
        }
        else
        {
            otherUI.SetActive(false);
        }

        creditUI.SetActive(true);
        isCreditOn = true;
    }

    public void UpdateScene(int index)
    {
        audioSource.Play();

        currentIndex += index;
        
        if (currentIndex >= creditTexts.Count - 1)
            currentIndex = creditTexts.Count - 1;

        if (currentIndex < 0)
            currentIndex = 0;

        for(int i =0; i< creditTexts.Count; i++)
        {
            if(i == currentIndex)
            {
                creditTexts[i].SetActive(true);
            }
            else
            {
                creditTexts[i].SetActive(false);
            }
        }
    }

    public void CloseCreditUI()
    {
        audioSource.Play();
        creditUI.SetActive(false);
        
        if(isMainGameOn && !tb.gameEnd)
        {
            pc.enabled = true;
            pm.enabled = true;
            bow.enabled = true;
            otherUI.SetActive(true);
        }
        else if(isMainGameOn && tb.gameEnd)
        {
            treasureBoxUI.SetActive(true);
        }
        else
        {
            otherUI.SetActive(true);
        }
        isCreditOn = false;
    }
}
