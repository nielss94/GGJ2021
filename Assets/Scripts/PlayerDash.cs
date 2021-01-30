using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using ECM.Controllers;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public static event Action OnDash = delegate {  };
    public static event Action OnStopDash = delegate {  };
    
    public float dashSpeed;
    public float dashSpeedRampUpDuration;
    public float dashDuration;
    public KeyCode dashKey;

    [ReadOnly] public float baseForwardSpeed; 
    
    private BaseFirstPersonController baseFirstPersonController;
    private bool dashing = false;

    private void Awake()
    {
        baseFirstPersonController = GetComponent<BaseFirstPersonController>();
        baseForwardSpeed = baseFirstPersonController.forwardSpeed;
    }

    private void OnValidate()
    {
        baseForwardSpeed = GetComponent<BaseFirstPersonController>().forwardSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey) && !dashing)
        {
            Dash();
        }
    }

    private void Dash()
    {
        dashing = true;
        OnDash?.Invoke();
        StartDash().OnComplete(() =>
        {
            StartCoroutine(StopDashAfterSeconds(dashDuration));
        });          
    }

    private IEnumerator StopDashAfterSeconds(float t)
    {
        yield return new WaitForSeconds(t);
        
        OnStopDash?.Invoke();
        baseFirstPersonController.forwardSpeed = baseForwardSpeed;
        dashing = false;
    }

    private TweenerCore<float, float, FloatOptions> StartDash()
    {
        return DOTween.To(() => baseFirstPersonController.forwardSpeed, x => baseFirstPersonController.forwardSpeed = x,
            dashSpeed, dashSpeedRampUpDuration);
    } 
}
