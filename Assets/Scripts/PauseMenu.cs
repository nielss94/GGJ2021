using System;
using System.Collections;
using System.Collections.Generic;
using ECM.Components;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public List<GameObject> itemsToDisable = new List<GameObject>();
    public GameObject settingsMenu;
    public GameObject pauseMenu;
    
    private MouseLook mouseLook;

    private void Awake()
    {
        mouseLook = FindObjectOfType<MouseLook>();
        
        GameManager.OnGamePaused += GameManagerOnOnGamePaused;
        GameManager.OnGameResumed += GameManagerOnOnGameResumed;
    }

    private void GameManagerOnOnGameResumed()
    {
        foreach (var disableGameObject in itemsToDisable)
        {
            disableGameObject.SetActive(true);
        }

        mouseLook.SetCursorLock(true);
    }

    private void GameManagerOnOnGamePaused()
    {
        foreach (var disableGameObject in itemsToDisable)
        {
            disableGameObject.SetActive(false);
        }

        mouseLook.SetCursorLock(false);
    }
    
    
    public void ShowSettings()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
}
