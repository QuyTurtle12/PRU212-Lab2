using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerManaer : MonoBehaviour
{
    public GameObject pauseMenuScreen;
    public static bool isGameOver;
    public GameObject gameOverScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Time.timeScale = 1;
        isGameOver = false;
    }
    public void Update()
    {
        if (isGameOver)
        {
            ScoreManager.Instance.SaveHighScore();
            SceneManager.LoadScene("EndMenu");
        }
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);
    }
    public void ResumGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
    }
    public void GotoMenu()
    {
        // Reset the score before loading the main menu
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }

        SceneManager.LoadScene("MainMenu");
    }
}
