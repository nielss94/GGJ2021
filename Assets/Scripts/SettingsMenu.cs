using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public AudioMixer audioMixer;
    public GameObject pauseMenu;

    private void Awake()
    {
        masterSlider.minValue = -80;
        masterSlider.maxValue = 20;

        masterSlider.value = PlayerPrefs.HasKey("MasterVolume") ? PlayerPrefs.GetFloat("MasterVolume") : -10;
        masterSlider.onValueChanged.AddListener((value) =>
        {
            PlayerPrefs.SetFloat("MasterVolume", value);
            audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
        });
        
        musicSlider.minValue = -80;
        musicSlider.maxValue = 20;

        musicSlider.value = PlayerPrefs.HasKey("MusicVolume") ? PlayerPrefs.GetFloat("MusicVolume") : -10;
        musicSlider.onValueChanged.AddListener((value) =>
        {
            PlayerPrefs.SetFloat("MusicVolume", value);
            audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        });
        
        sfxSlider.minValue = -80;
        sfxSlider.maxValue = 20;

        sfxSlider.value = PlayerPrefs.HasKey("SFXVolume") ? PlayerPrefs.GetFloat("SFXVolume") : -10;
        sfxSlider.onValueChanged.AddListener((value) =>
        {
            PlayerPrefs.SetFloat("SFXVolume", value);
            audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
        });
    }

    public void Close()
    {
        pauseMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
