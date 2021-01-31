using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject mainMenu;
    public GameObject creditMenu;
    
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowSettings()
    {
        settingsMenu.SetActive(true);
        
        mainMenu.SetActive(false);
        creditMenu.SetActive(false);
    }

    public void ShowCredits()
    {
        
        creditMenu.SetActive(true);
        
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void ShowMainMenu()
    {
        
        mainMenu.SetActive(true);
        
        creditMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }
}
