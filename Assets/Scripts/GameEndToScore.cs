using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndToScore : MonoBehaviour
{
    private void Awake()
    {
        GameManager.OnGameEnded += GoToScore;
    }

    private void GoToScore()
    {
        SceneManager.LoadScene("Score");
    }
}
