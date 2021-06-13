﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{

    public void RestartButton()
    {
        SceneManager.LoadScene("MainGameScene");
    }


    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenuScene");

    }
}
