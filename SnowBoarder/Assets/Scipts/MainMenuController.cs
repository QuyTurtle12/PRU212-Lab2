using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject instructionPanel;
    public GameObject mainMenuPanel;
    public string level1SceneName = "Level1";
    public void ShowInstructionPanel()
    {
        instructionPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void HideInstructionPanel()
    {
        instructionPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void LoadLevel1()
    {
        if (!string.IsNullOrEmpty(level1SceneName))
        {
            SceneManager.LoadScene(level1SceneName);
        }
        else
        {
            Debug.LogError("Level 1 Scene Name is not set in the MainMenuController script!");
        }
    }
}