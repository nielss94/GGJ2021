using System;
using System.Collections;
using System.Collections.Generic;
using ECM.Controllers;
using UnityEngine;

public class PlayerSlow : MonoBehaviour {

    public float slowAmount = 0.5f;

    private BaseFirstPersonController controller;
    private bool isSlowed = false;
    private float initialForwardSpeed;
    private float initialBackwardSpeed;
    private float initialStrafeSpeed;
    
    private void Start() {
        PlayerDash.OnStopDash += CheckSlow;
        controller = GetComponent<BaseFirstPersonController>();
        initialForwardSpeed = controller.forwardSpeed;
        initialBackwardSpeed = controller.backwardSpeed;
        initialStrafeSpeed = controller.strafeSpeed;
    }

    public void EnableSlow() {
        isSlowed = true;
        controller.forwardSpeed = initialForwardSpeed * slowAmount;
        controller.backwardSpeed = initialBackwardSpeed * slowAmount;
        controller.strafeSpeed = initialStrafeSpeed * slowAmount;
    }

    public void DisableSlow() {
        isSlowed = false;
        controller.forwardSpeed = initialForwardSpeed;
        controller.backwardSpeed = initialBackwardSpeed;
        controller.strafeSpeed = initialStrafeSpeed;
    }

    private void CheckSlow() {
        if (isSlowed) {
            controller.forwardSpeed = initialForwardSpeed * slowAmount;
            controller.backwardSpeed = initialBackwardSpeed * slowAmount;
            controller.strafeSpeed = initialStrafeSpeed * slowAmount;
        }
    }
}
