using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using ECM.Controllers;
using UnityEngine;
using UnityEngine.SearchService;

public class PlayerDash : MonoBehaviour
{
    public static event Action OnDash = delegate {  };
    public static event Action OnStopDash = delegate {  };
    

    [Header("Dash")]
    public float dashKnockbackForce;
    public float dashCooldown;
    public float dashSpeed;
    public float dashSpeedRampUpDuration;
    public float dashDuration;
    public KeyCode dashKey;
    public GameObject dashHitEffectPrefab;
    public Vector3 dashEffectOffset;
    [ReadOnly] public float baseForwardSpeed; 
    
    private BaseFirstPersonController baseFirstPersonController;
    private bool dashing = false;
    private bool isCrouching = false;
    
    public static event Action<float, float> OnDashTimerChanged = delegate {  };
    private float dashTimer;

    private void Awake()
    {
        baseFirstPersonController = GetComponent<BaseFirstPersonController>();
        baseForwardSpeed = baseFirstPersonController.forwardSpeed;

        BaseCharacterController.OnSetCrouch += Crouch;
    }

    private void Crouch(bool b)
    {
        isCrouching = b;
    }

    private void OnValidate()
    {
        baseForwardSpeed = GetComponent<BaseFirstPersonController>().forwardSpeed;
    }

    private void Update()
    {
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
            
            OnDashTimerChanged.Invoke(dashTimer < 0 ? 0 : dashTimer, dashCooldown);
        }
        else
        {
            dashTimer = 0;
        }
        
        if (Input.GetAxisRaw("Dash") > 0 && !dashing && dashTimer == 0)
        {
            Dash();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Child child) && dashing)
        {
            if (child.canGetKnockedDown) {
                Vector3 effectPos = other.transform.position + dashEffectOffset;
                Instantiate(dashHitEffectPrefab, effectPos, Quaternion.identity);
                Vector3 normalizedAngle = (child.transform.position - transform.position) + Vector3.up + transform.TransformDirection(Vector3.forward).normalized;
                child.KnockBack(normalizedAngle.normalized, dashKnockbackForce);
            }
        }
    }

    private void Dash()
    {
        if (isCrouching)
        {
            return;
        }

        dashing = true;
        OnDash?.Invoke();
        dashTimer = dashCooldown;
        StartDash().OnComplete(() =>
        {
            StartCoroutine(StopDashAfterSeconds(dashDuration));
        });          
    }

    private IEnumerator StopDashAfterSeconds(float t)
    {
        yield return new WaitForSeconds(t);
        
        baseFirstPersonController.forwardSpeed = baseForwardSpeed;
        OnStopDash?.Invoke();
        dashing = false;
    }

    private TweenerCore<float, float, FloatOptions> StartDash()
    {
        return DOTween.To(() => baseFirstPersonController.forwardSpeed, x => baseFirstPersonController.forwardSpeed = x,
            dashSpeed, dashSpeedRampUpDuration);
    }

    private void OnDestroy()
    {
        BaseCharacterController.OnSetCrouch -= Crouch;
    }
}
