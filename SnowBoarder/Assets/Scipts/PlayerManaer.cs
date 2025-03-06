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
        isGameOver = false;
    }
    public void Update()
    {
        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
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
        SceneManager.LoadScene("MainMenu");
    }
}
