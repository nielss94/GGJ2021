using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }
    
    public static event Action OnGameStarted = delegate { };
    public static event Action OnGamePaused = delegate { };
    public static event Action OnGameResumed = delegate { };
    public static event Action OnGameEnded = delegate { };

    [SerializeField] private RectTransform pauseMenu;

    [SerializeField] private KeyCode pauseKey;

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
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            PauseGame();
        }
    }
    
    public void ResumeGame()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        OnGameResumed?.Invoke();
    }

    public void EndGame()
    {
        OnGameEnded?.Invoke();
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void StartGame()
    {
        OnGameStarted?.Invoke();
    }

    private void PauseGame()
    {
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
        OnGamePaused?.Invoke();
    }

}
