using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LostChildUI : MonoBehaviour
{
    [SerializeField] private RawImage renderImage;
    [SerializeField] private LostChildRenderer lostChildRenderer;
    private void Awake()
    {
        LostChildSystem.OnNewChildSelected += NewChildSelected;
    }

    private void NewChildSelected(Child child)
    {
        renderImage.DOFade(0, .5f)
            .OnComplete(() =>
            {
                lostChildRenderer.NewChild(child);
                renderImage.DOFade(1, .5f);
            });
    }
}
