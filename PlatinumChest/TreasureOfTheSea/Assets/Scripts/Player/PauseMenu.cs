using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject pauseMenu;
    public bool isGamePasued;
    public Texture2D cursorTexture;
    private Bow bow;
    private PlayerManager pm;
    private PlayerController pc;
    public GameObject warningUI;

    // Start is called before the first frame update
    void Start()
    {
        isGamePasued = false;
        pauseMenu.SetActive(false);
        warningUI.SetActive(false);

        bow = GameObject.FindWithTag("Bow").GetComponent<Bow>();
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.CapsLock))
#else
        if(Input.GetKeyDown(KeyCode.Escape))
#endif
        {
            Pause();
        }
    }

    public void Resume()
    {
        audioSource.Play();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePasued = false;
        pc.enabled = true;
        pm.enabled = true;
        bow.enabled = true;
        isGamePasued = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        pc.enabled = false;
        pm.enabled = false;
        bow.enabled = false;

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        
        isGamePasued = true;
    }

    public void WarningAlert()
    {
        audioSource.Play();
        pauseMenu.SetActive(false);
        warningUI.SetActive(true);
    }

    public void CloseWarning()
    {
        audioSource.Play();
        pauseMenu.SetActive(true);
        warningUI.SetActive(false);
    }

    public void QuitGame()
    {
        audioSource.Play();
        Application.Quit();
    }
}
