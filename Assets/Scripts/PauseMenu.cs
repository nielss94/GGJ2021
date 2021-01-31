using System;
using System.Collections;
using System.Collections.Generic;
using ECM.Components;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public List<GameObject> itemsToDisable = new List<GameObject>();
    
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
}
