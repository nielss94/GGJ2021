using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DashUI : MonoBehaviour
{
    public Image speedLines;
    
    private void Awake()
    {
        PlayerDash.OnDash += DoDashAnimation;
        PlayerDash.OnStopDash += StopDashAnimation;
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
        DOTween.Sequence()
            .Append(speedLines.DOFade(.3f, .3f))
            .Append(speedLines.transform.DOScale(Vector3.one * 2.8f, 1f));
    }
}
