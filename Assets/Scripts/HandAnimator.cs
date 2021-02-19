using System;
using System.Collections;
using System.Collections.Generic;
using ECM.Controllers;
using UnityEngine;

public class HandAnimator : MonoBehaviour
{
    private Animator animator;

    private bool holdingChild = false;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();

        PlayerDash.OnDash += Dash;
        BaseCharacterController.OnSetCrouch += Crouch;
        BaseFirstPersonController.OnSetRunning += Run;
        PlayerStun.OnSetStunned += Stun;
        PlayerInteraction.OnTakeChild += SetHoldingChild;
        PlayerInteraction.OnDeliverChild += SetNotHoldingChild;
    }


    private void Dash()
    {
        animator.SetTrigger("doDash");
    }
    
    private void Crouch(bool b)
    {
        animator.SetBool("isCrouching", b);
    }
    private void Run(bool b)
    {
        animator.SetBool("isRunning", b);
    }
    
    private void Stun(bool b)
    {
        animator.SetBool("isStunned", b);
    }

    private void SetHoldingChild(Child child)
    {
        animator.SetBool("holdingChild", true);
    }
    
    private void SetNotHoldingChild()
    {
        animator.SetBool("holdingChild", false);
    }
    
    private void OnDestroy()
    {
        PlayerDash.OnDash -= Dash;
        BaseCharacterController.OnSetCrouch -= Crouch;
        BaseFirstPersonController.OnSetRunning -= Run;
        PlayerStun.OnSetStunned -= Stun;
        PlayerInteraction.OnTakeChild -= SetHoldingChild;
    }
}
