using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public AudioSource audioSource;

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
        Debug.Log("Quitting");
    }
}
