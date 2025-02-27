using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject instructionPanel;
    public GameObject mainMenuPanel;
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
}