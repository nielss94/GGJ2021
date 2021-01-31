using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OrderedPatrol : MonoBehaviour
{
    private NavTarget[] navTargets;
    private NavTargetManager navTargetManager;
    private int destIndex;
    private NavMeshAgent agent;
    private ChildNavAgent childNavAgent;
    private bool isNavigating = false;
    
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        childNavAgent = GetComponent<ChildNavAgent>();
        childNavAgent.OnStartNavigation += StartNavigation;
        childNavAgent.OnEndNavigation += StopNavigation;
    }

    private void StartNavigation() {
        navTargetManager = NavTargetManager.Instance;
        navTargets = navTargetManager.GetAllNavTargets();
        isNavigating = true;
        
        GoToNextPoint();
    }

    private void StopNavigation() {
        isNavigating = false;
        agent.isStopped = true;
    } 

    private void Update() {
        if (isNavigating &&
            agent.enabled &&
            !agent.pathPending &&
            agent.remainingDistance < 0.5f) {
            GoToNextPoint();
        }
    }

    private void GoToNextPoint() {
        if (navTargets.Length == 0)
            return;

        agent.destination = navTargets[destIndex].transform.position;
        destIndex = (destIndex + 1) % navTargets.Length;
    }

    private void OnDestroy() {
        childNavAgent.OnStartNavigation -= StartNavigation;
        childNavAgent.OnEndNavigation -= StopNavigation;
    }
}