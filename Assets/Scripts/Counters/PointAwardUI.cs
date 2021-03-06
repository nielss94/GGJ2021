using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PointAwardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointAwardText;
    private Color standardTextColor;
    private Color standardTransparentColor;

    [Header("Motivation text")]
    [SerializeField] private string shortText = "Perfect";
    [SerializeField] private string mediumText = "Good";
    [SerializeField] private string longText = "Too slow";

    [Header("Flash timing")] 
    [SerializeField]
    private float showTime = 0.75f;
    [SerializeField]
    private float hideTime = 0.75f;
    
    private void Awake()
    {
        PointAwardSystem.OnShort += OnShort;
        PointAwardSystem.OnMedium += OnMedium;
        PointAwardSystem.OnLong += OnLong;

        standardTextColor = pointAwardText.color;        
        standardTransparentColor = pointAwardText.color;
        standardTransparentColor.a = 0;

        pointAwardText.color = standardTransparentColor;
    }

    private void OnShort(int points)
    {
        pointAwardText.text = shortText + " +" + points;
        FlashAward();
    }
    
    private void OnMedium(int points)
    {
        pointAwardText.text = mediumText + " +" + points;
        FlashAward();
    }
    
    private void OnLong(int points)
    {
        pointAwardText.text = longText + " +" + points;
        FlashAward();
    }

    private void FlashAward()
    {
        DOTween.Sequence()
            .Append(pointAwardText.DOColor(standardTextColor, showTime))
            .Append(pointAwardText.DOColor(standardTransparentColor, hideTime));
    }

    private void OnDestroy()
    {
        PointAwardSystem.OnShort -= OnShort;
        PointAwardSystem.OnMedium -= OnMedium;
        PointAwardSystem.OnLong -= OnLong;
    }
}
