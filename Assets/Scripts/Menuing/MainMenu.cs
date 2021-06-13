using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsPanel;
    public GameObject tutorialPanel;
    private void Start()
    {
        creditsPanel.SetActive(false);
        tutorialPanel.SetActive(false);
    }
    public void SwitchToMainScene()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void OpenTutPanel()
    {
        tutorialPanel.SetActive(true);
    }

    public void CloseTutPanel()
    {
        tutorialPanel.SetActive(false);
    }

    public void OpenCreditsPanel()
    {
        creditsPanel.SetActive(true);
    }

    public void CloseCreditsPanel()
    {
        creditsPanel.SetActive(false);
    }
}
