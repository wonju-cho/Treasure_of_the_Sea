using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isGamePasued;
    public Texture2D cursorTexture;
    private Bow bow;
    private PlayerManager pm;
    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        isGamePasued = false;
        pauseMenu.SetActive(false);
        bow = GameObject.FindWithTag("Bow").GetComponent<Bow>();
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();

        if (!pauseMenu)
            Debug.Log("there is no pause menu in the pause menu script");
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

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quitting game");
    }
}
