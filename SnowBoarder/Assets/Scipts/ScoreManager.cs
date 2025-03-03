using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;

    public int currentScore = 0;
    public int highScore = 0;
    public int comboCount = 0;
    public float scoreMultiplier = 1f;

    // For combo system: reset combo if no trick in this interval
    public float comboResetTime = 2.0f;
    private float comboTimer = 0f;

    private void Awake()
    {
        // Singleton pattern to persist ScoreManager across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Look for the TMP Text component by its name "ScoreText"
        GameObject scoreTextObj = GameObject.Find("ScoreText");
        if (scoreTextObj != null)
        {
            scoreText = scoreTextObj.GetComponent<TextMeshProUGUI>();
            // Update the UI right away with the current score.
            UpdateScoreUI();
        }
        else
        {
            Debug.LogWarning("ScoreText object not found in scene: " + scene.name);
        }
    }

    private void Start()
    {
        // Load the saved high score
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreUI();
    }

    private void Update()
    {
        // If a combo is active, increment timer and reset combo if time runs out.
        if (comboCount > 0)
        {
            comboTimer += Time.deltaTime;
            if (comboTimer > comboResetTime)
            {
                ResetCombo();
            }
        }
    }

    // Score based on speed (for example, converting speed to points)
    public void AddSpeedScore(float speed)
    {
        int points = Mathf.RoundToInt(speed * 10f); // tweak multiplier as needed
        AddScore(points);
    }

    // Score for collecting items like snowflakes
    public void AddItemScore(int itemPoints)
    {
        AddScore(itemPoints);
    }

    // Score for performing a trick. This also updates the combo count and multiplier.
    public void AddTrickScore(int trickBasePoints)
    {
        // Each successful trick increases the combo count.
        comboCount++;
        comboTimer = 0f;  // reset the combo timer
        UpdateMultiplier();
        int totalPoints = Mathf.RoundToInt(trickBasePoints * scoreMultiplier);
        AddScore(totalPoints);
    }

    // Common method to update the current score.
    void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreUI();
        Debug.Log("Added " + points + " points. Total Score: " + currentScore);
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
    }

    // Reset the combo count and multiplier.
    public void ResetCombo()
    {
        comboCount = 0;
        scoreMultiplier = 1f;
        comboTimer = 0f;
    }

    // Increase multiplier based on current combo count.
    void UpdateMultiplier()
    {
        // For instance, each trick increases the multiplier by 0.1.
        scoreMultiplier = 1f + (comboCount * 0.1f);
    }

    // Save the high score using PlayerPrefs.
    public void SaveHighScore()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            Debug.Log("New High Score: " + highScore);
        }
    }
}
