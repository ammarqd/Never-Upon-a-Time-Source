using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject controlsPanel;
    public GameObject menuPanel;
    public void PlayGame() {
        SceneManager.LoadScene("Cutscene");
    }
    public void ViewControls()
    {
        controlsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }
    public void GoToMenu()
    {
        menuPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }
    public void EndToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
