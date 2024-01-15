using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;

    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Debug.Log("Game Over!");
    }

    public void Restart()
    {
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
