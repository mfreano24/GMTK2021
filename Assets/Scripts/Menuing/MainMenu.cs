using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsPanel;
    private void Start()
    {
        creditsPanel.SetActive(false);
    }
    public void SwitchToMainScene()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void SwitchToTutScene()
    {
        SceneManager.LoadScene("TutorialScene");
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
