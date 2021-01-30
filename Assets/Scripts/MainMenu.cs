using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown = null;
    private string selectedScene;

    private void Awake()
    {
        SetDropDownScenes();
    }

    public void StartSmallGame()
    {
        SceneManager.LoadScene(selectedScene != "" ? selectedScene : "Game");
    }
    
    public void StartMediumGame()
    {
        SceneManager.LoadScene(selectedScene != "" ? selectedScene : "Game");
    }
    
    public void StartLargeGame()
    {
        SceneManager.LoadScene(selectedScene != "" ? selectedScene : "Game");
    }
    
    private void SetDropDownScenes()
    {
        dropdown.ClearOptions();

        List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
        
        optionDataList.Add(new TMP_Dropdown.OptionData("Select-Level"));
        
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string name = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            if (name != "Menu")
            {
                optionDataList.Add(new TMP_Dropdown.OptionData(name));
            } 
        }
 
        dropdown.AddOptions(optionDataList);
    }
    
    public void SelectScene(){
        selectedScene = dropdown.options[dropdown.value].text;
    }
}
