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

    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.position;

        rectTransform.position = new Vector3(-380f, originalPosition.y, originalPosition.z);
        
        LostChildSystem.OnNewChildSelected += NewChildSelected;
        PlayerInteraction.OnTakeChild += RemoveChildFromUI;
    }

    private void NewChildSelected(Child child)
    {
        rectTransform.DOMoveX(originalPosition.x, 0.5f);
        
        renderImage.DOFade(0, .5f)
            .OnComplete(() =>
            {
                lostChildRenderer.NewChild(child);
                renderImage.DOFade(1, .5f);
            });
    }

    private void RemoveChildFromUI(Child child)
    {
        
        rectTransform.DOMoveX(-380f, 0.5f);
        
        renderImage.DOFade(0, .5f)
            .OnComplete(() =>
            {
                lostChildRenderer.RemoveChild();
                renderImage.DOFade(1, .5f);
            });
    }
}
