using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game 1");
    }
}
