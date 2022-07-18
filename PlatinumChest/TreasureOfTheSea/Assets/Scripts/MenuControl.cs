using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public string firstLevel;
    public string creditLevel;
    public string mainLevel;

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void CreditScreen()
    {
        SceneManager.LoadScene(creditLevel);
    }

    public void MainMenuScreen()
    {
        SceneManager.LoadScene(mainLevel);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
