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
        PlayerInteraction.OnTakeChild += RemoveChildFromUI;
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

    private void RemoveChildFromUI(Child child)
    {
        renderImage.DOFade(0, .5f)
            .OnComplete(() =>
            {
                lostChildRenderer.RemoveChild();
                renderImage.DOFade(1, .5f);
            });
    }
}
