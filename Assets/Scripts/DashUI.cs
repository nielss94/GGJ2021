using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashUI : MonoBehaviour
{
    [SerializeField] private RectTransform percentageBar;
    [SerializeField] private CanvasGroup dashIcon;
    public Image speedLines;
    
    private void Awake()
    {
        PlayerDash.OnDash += DoDashAnimation;
        PlayerDash.OnStopDash += StopDashAnimation;
        PlayerDash.OnDashTimerChanged += ChangeDashBar;
    }

    private void ChangeDashBar(float timer, float max)
    {
        float percentage = timer / max;

        percentageBar.DOScaleX(1 - percentage, 0.1f);

        if (percentage == 0 && dashIcon.alpha == 0)
        {
            dashIcon.DOFade(1, 0.1f);
        }
    }

    private void StopDashAnimation()
    {
        DOTween.Sequence()
            .Append(speedLines.DOFade(0f, .2f)
                .OnComplete(() => speedLines.transform.DOScale(Vector3.one * 1.2f, .8f)
                ));
    }

    private void DoDashAnimation()
    {
        dashIcon.DOFade(0, 0.1f);
        
        DOTween.Sequence()
            .Append(speedLines.DOFade(.3f, .3f))
            .Append(speedLines.transform.DOScale(Vector3.one * 2.8f, 1f));
    }
}
