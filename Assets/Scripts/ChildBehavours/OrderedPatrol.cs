using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OrderedPatrol : MonoBehaviour
{
    public NavTarget[] NavTargets;
    public float WaitTime = 0f;
    
    private NavTargetManager navTargetManager;
    private int destIndex;
    private NavMeshAgent agent;
    private ChildNavAgent childNavAgent;
    private bool isNavigating = false;
    private bool isWaiting = false;
    
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        childNavAgent = GetComponent<ChildNavAgent>();
        childNavAgent.OnStartNavigation += StartNavigation;
        childNavAgent.OnEndNavigation += StopNavigation;
    }

    private void StartNavigation() {
        if (NavTargets.Length == 0) {
            navTargetManager = NavTargetManager.Instance;
            NavTargets = navTargetManager.GetAllNavTargets();
        }
        isNavigating = true;
        StartCoroutine(GoToNextPoint());
    }

    private void StopNavigation() {
        isNavigating = false;
        agent.isStopped = true;
    } 

    private void Update() {
        
        if (!isWaiting &&
            isNavigating &&
            agent.enabled &&
            !agent.pathPending &&
            agent.remainingDistance < 0.5f) {
            StartCoroutine(GoToNextPoint());
        }
    }

    private IEnumerator GoToNextPoint() {
        isWaiting = true;
        yield return new WaitForSeconds(WaitTime);
        if (NavTargets.Length == 0) {
            isWaiting = false;
            yield break;
        }

        agent.destination = NavTargets[destIndex].transform.position;
        destIndex = (destIndex + 1) % NavTargets.Length;
        isWaiting = false;
    }

    private void OnDestroy() {
        childNavAgent.OnStartNavigation -= StartNavigation;
        childNavAgent.OnEndNavigation -= StopNavigation;
    }
}