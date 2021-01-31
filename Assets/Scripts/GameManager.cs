using System;
using System.Collections;
using System.Collections.Generic;
using ECM.Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public static event Action OnGameStarted = delegate { };
    public static event Action OnGamePaused = delegate { };
    public static event Action OnGameResumed = delegate { };
    public static event Action OnGameEnded = delegate { };

    [SerializeField] private RectTransform pauseContainer;
    [SerializeField] private RectTransform settings;

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Debug.Log("PAUSE");
            
            if (Time.timeScale == 0)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();    
            }
        }
    }
    
    public void ResumeGame()
    {
        pauseContainer.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);
        Time.timeScale = 1;
        OnGameResumed?.Invoke();
    }

    public static void EndGame()
    {
        OnGameEnded?.Invoke();
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void Settings()
    {
        settings.gameObject.SetActive(true);
        pauseContainer.gameObject.SetActive(false);
    }
    
    private void StartGame()
    {
        OnGameStarted?.Invoke();
    }

    private void PauseGame()
    {
        pauseContainer.gameObject.SetActive(true);
        Time.timeScale = 0;
        OnGamePaused?.Invoke();
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
