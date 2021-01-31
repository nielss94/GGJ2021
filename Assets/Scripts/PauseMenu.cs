using System;
using System.Collections;
using System.Collections.Generic;
using ECM.Components;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private MouseLook mouseLook;

    private void Awake()
    {
        mouseLook = FindObjectOfType<MouseLook>();
        
        GameManager.OnGamePaused += GameManagerOnOnGamePaused;
        GameManager.OnGameResumed += GameManagerOnOnGameResumed;
    }

    private void GameManagerOnOnGameResumed()
    {
        mouseLook.SetCursorLock(true);
    }

    private void GameManagerOnOnGamePaused()
    {        
        mouseLook.SetCursorLock(false);
    }
}
