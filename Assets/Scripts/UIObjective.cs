using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public delegate void FadeAction();

public class UIObjective : MonoBehaviour
{
    [SerializeField] private Transform uiCameraTransform;

    private CanvasGroup canvasGroup;

    private bool isFading = false;
    private bool isShowing = false;
    private bool shouldBlockNew = false;

    private Queue<FadeAction> fadeQueue = new Queue<FadeAction>() { };
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update() 
    {
        // look at camera...
        transform.LookAt(uiCameraTransform.position, -Vector3.up);
        // then lock rotation to Y axis only...
        transform.localEulerAngles = new Vector3(0,transform.localEulerAngles.y,0);

        if (!isFading && fadeQueue.Count > 0)
        {
            fadeQueue.Dequeue().Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FadeTransparentObjective();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FadeInObjective();
        }
    }

    public void DoShow()
    {
        isShowing = true;
    }

    public void FadeTransparentObjective()
    {
        Fade(0.25f, 0.5f);
    }
    
    public void FadeOutObjective(bool hideAfter = false)
    {
        Fade(0f, 0.5f, hideAfter);
    }
    
    public void FadeInObjective()
    {
        Fade(0.8f, 0.5f);
    }

    private void Fade(float value, float duration, bool hideAfter = false)
    { 
        if (!isShowing || shouldBlockNew)
        {
            return;
        }
        
        if (hideAfter)
        {
            shouldBlockNew = true;
        }

        if (isFading)
        {
            fadeQueue.Enqueue(() => Fade(value, duration));
            return;
        }
        
        canvasGroup
            .DOFade(value, duration)
            .OnComplete(() =>
            {
                isFading = false;

                if (hideAfter)
                {
                    fadeQueue.Clear();
                    isShowing = false;
                    shouldBlockNew = false;
                }
            });
    }
}
