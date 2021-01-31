using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private TextMeshProUGUI deliverInteractionText;
    [SerializeField] private TextMeshProUGUI takeChildText;
    
    [SerializeField] private float interactionResultFadeTime = 1;
    [SerializeField] private float interactionResultOnScreenTime = 1;
    
    private void Start()
    {
        PlayerInteraction.OnInteraction += ShowInteraction;
        PlayerInteraction.OnTakeChild += ShowTakeChild;
        PlayerInteraction.OnFailTakeChild += ShowFailTakeChild;
        PlayerInteraction.OnDeliverInteraction += ShowDeliverInteraction;
    }
    

    private void ShowTakeChild(Child child)
    {
        takeChildText.text = "yoink!";
        StartCoroutine(FadeTakeChild());
    }

    private void ShowFailTakeChild()
    {
        takeChildText.text = "Wrong child!";
        StartCoroutine(FadeFailTakeChild());
    }

    private IEnumerator FadeFailTakeChild()
    {
        takeChildText.DOFade(1, interactionResultFadeTime);
        
        yield return new WaitForSeconds(interactionResultOnScreenTime);
        
        takeChildText.DOFade(0, interactionResultFadeTime);
    }
    
    private IEnumerator FadeTakeChild()
    {
        takeChildText.DOFade(1, interactionResultFadeTime);
        
        yield return new WaitForSeconds(interactionResultOnScreenTime);
        
        takeChildText.DOFade(0, interactionResultFadeTime);
    }

    private void ShowInteraction(Child child)
    {
        if (!child)
        {
            interactionText.enabled = false;
            return;
        }

        interactionText.text = "Press E to interact";
        interactionText.enabled = true;
    }
    
    private void ShowDeliverInteraction(bool show)
    {
        if (!show)
        {
            deliverInteractionText.enabled = false;
            return;
        }

        deliverInteractionText.enabled = true;
    }

    private void OnDestroy()
    {
        PlayerInteraction.OnInteraction -= ShowInteraction;
        PlayerInteraction.OnTakeChild -= ShowTakeChild;
        PlayerInteraction.OnFailTakeChild -= ShowFailTakeChild;
        PlayerInteraction.OnDeliverInteraction -= ShowDeliverInteraction;
    }
}
