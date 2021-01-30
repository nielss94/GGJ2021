using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTimeEndDoGameEnd : MonoBehaviour
{
    private void Awake()
    {
        TimeSystem.OnTimeEnded += EndGame;
    }

    private void EndGame()
    {
        GameManager.EndGame();
    }
}
