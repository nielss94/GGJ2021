using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class IntroToGameSystem : MonoBehaviour
{
    [Header("Settings")] 
    public float fadeOutTime;
    public float fadeInTime;
    
    public CanvasGroup intro;
    public CanvasGroup hud;

    private void Awake()
    {
        hud.alpha = 0;
        intro.alpha = 0;
        GameManager.OnGameStarted += ShowIntro;
        IntroDialog.OnIntroComplete += ShowHUD;
    }

    private void ShowIntro()
    {
        intro.DOFade(1, fadeInTime);
        hud.DOFade(0, fadeOutTime);
    }

    private void ShowHUD()
    {
        intro.DOFade(0, fadeOutTime);
        hud.DOFade(1, fadeInTime);
    }
}
