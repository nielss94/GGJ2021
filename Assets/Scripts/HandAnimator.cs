using System;
using System.Collections;
using System.Collections.Generic;
using ECM.Controllers;
using UnityEngine;

public class HandAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        PlayerDash.OnDash += () => animator.SetTrigger("doDash");
        BaseCharacterController.OnSetCrouch += b => animator.SetBool("isCrouching", b);
        BaseFirstPersonController.OnSetRunning += b => animator.SetBool("isRunning", b);
        PlayerStun.OnSetStunned += b => animator.SetBool("isStunned", b);
    }
}
