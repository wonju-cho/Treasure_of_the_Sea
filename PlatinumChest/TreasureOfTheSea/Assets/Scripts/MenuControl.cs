using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public AudioSource audioSource;
    public Texture2D cursorTexture;
    public string firstLevel;
    public string mainMenuLevel;
    public void StartGame()
    {
        audioSource.Play();
        SceneManager.LoadScene(firstLevel);
    }

    public void MainMenuScreen()
    {
        SceneManager.LoadScene(mainMenuLevel);
    }

    public void QuitGame()
    {
        audioSource.Play();
        Application.Quit();
    }

    private void Update()
    {
        Cursor.visible = true;
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Confined;        
    }
}
