using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class OrderedPatrol : MonoBehaviour
{
    public List<NavTarget> NavTargets;
    public float WaitTime = 0f;
    public float approachThreshold = 0.5f;
    
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
        if (NavTargets.Count == 0) {
            navTargetManager = NavTargetManager.Instance;
            NavTargets = navTargetManager.GetAllNavTargets();
            ShuffleNavTargets();
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
            agent.remainingDistance < approachThreshold) {
            StartCoroutine(GoToNextPoint());
        }
    }

    private IEnumerator GoToNextPoint() {
        isWaiting = true;
        yield return new WaitForSeconds(WaitTime);
        if (NavTargets.Count == 0) {
            isWaiting = false;
            yield break;
        }

        if (NavTargets.Count > 0) {
            if (agent.enabled) agent.destination = NavTargets[destIndex].transform.position;
        } else {
            Debug.LogWarning("Agent is missing nav-targets.");
        }
        destIndex = (destIndex + 1) % NavTargets.Count;
        isWaiting = false;
    }

    private void ShuffleNavTargets() {
        for (int i = 0; i < NavTargets.Count; i++) {
            var temp = NavTargets[i];
            int swapIndex = Random.Range(i, NavTargets.Count);
            NavTargets[i] = NavTargets[Random.Range(i, swapIndex)];
            NavTargets[swapIndex] = temp;
        }
    }

    private void OnDestroy() {
        if (childNavAgent) {
            childNavAgent.OnStartNavigation -= StartNavigation;
            childNavAgent.OnEndNavigation -= StopNavigation;
        }
    }
}