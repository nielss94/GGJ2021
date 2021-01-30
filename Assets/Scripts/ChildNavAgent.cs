using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChildNavAgent : MonoBehaviour {
    public Transform[] navTargets;

    private Animator anim;
    private NavMeshAgent agent;
    private int destIndex;
    
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        GoToNextPoint();
    }
    
    private void Update() {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GoToNextPoint();
        
        Vector3 velocity = agent.velocity;

        bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;
        float velocityMag = velocity.magnitude;
        anim.SetBool("move", shouldMove);
        anim.SetFloat("velocity", velocityMag);
    }

    private void GoToNextPoint() {
        if (navTargets.Length == 0)
            return;

        agent.destination = navTargets[destIndex].position;
        destIndex = (destIndex + 1) % navTargets.Length;
    }

}
