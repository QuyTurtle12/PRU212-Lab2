using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndMenuController : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to your Score Text UI element
    public TextMeshProUGUI highScoreText; // Reference to your High Score Text UI element

    public string mainMenuSceneName = "MainMenu"; // Name of your Main Menu scene

    private void Start()
    {
        // Display the current score and high score
        if (scoreText != null && highScoreText != null)
        {
            DisplayScores();
        }
        else
        {
            Debug.LogError("Score or High Score Text elements not assigned in the EndMenu script!");
        }
    }

    public void DisplayScores()
    {
        if (ScoreManager.Instance != null)
        {
            scoreText.text = "Score: " + ScoreManager.Instance.currentScore.ToString();
            highScoreText.text = "Highest Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        }
        else
        {
            Debug.LogError("ScoreManager.Instance is null! Make sure ScoreManager is present in the scene.");
        }
    }

    public void ReturnToMainMenu()
    {
        // Reset the score (optional, depending on your game design)
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }

        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game!");
        Application.Quit();
    }
}