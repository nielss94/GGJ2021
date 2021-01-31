using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChildNavAgent : MonoBehaviour {
    public event Action OnStartNavigation = delegate { };
    public event Action OnEndNavigation = delegate { };

    private Animator anim;
    private NavMeshAgent agent;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        OnStartNavigation.Invoke();
    }
    
    private void Update() {
        Vector3 velocity = agent.velocity;

        bool shouldMove = velocity.magnitude > 0.2f && agent.remainingDistance > agent.radius;
        float velocityMag = velocity.magnitude;
        anim.SetBool("move", shouldMove);
        anim.SetFloat("velocity", velocityMag);
    }

}
