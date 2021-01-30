using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameOnTimeSystem : MonoBehaviour
{    
    public static EndGameOnTimeSystem Instance { get; private set; }

    private Scene endScene;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        TimeSystem.OnTimeEnded += EndGame;
    }

    private void EndGame()
    {
        GameManager.Instance.EndGame();
    }
}
