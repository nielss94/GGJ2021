using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class RandomPatrol : MonoBehaviour {
    public NavTarget[] NavTargets;
    public float WaitTime = 0f;
    public float approachThreshold = 0.5f;
    
    private NavTargetManager navTargetManager;
    private NavMeshAgent agent;
    private ChildNavAgent childNavAgent;
    private bool isNavigating = false;
    private bool isWaiting = false;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        childNavAgent = GetComponent<ChildNavAgent>();
        childNavAgent.OnStartNavigation += StartNavigation;
        childNavAgent.OnEndNavigation += StopNavigation;
        StartCoroutine(GoToRandomPoint());
    }

    private void StartNavigation() {
        if (NavTargets.Length == 0) {
            navTargetManager = NavTargetManager.Instance;
            NavTargets = navTargetManager.GetAllNavTargets();
        }
        isNavigating = true;
    }

    private void Update() {
        if (!isWaiting &&
            isNavigating &&
            agent.enabled &&
            !agent.pathPending &&
            agent.remainingDistance < approachThreshold) {
            StartCoroutine(GoToRandomPoint());
        }
    }

    private IEnumerator GoToRandomPoint() {
        isWaiting = true;
        yield return new WaitForSeconds(WaitTime);
        if (NavTargets.Length == 0) {
            isWaiting = false;
            yield break;
        }

        if (NavTargets.Length > 0) {
            if (agent.enabled) agent.destination = NavTargets[Random.Range(0, NavTargets.Length - 1)].transform.position;
        } else {
            Debug.LogWarning("Agent is missing nav-targets.");
        }
        isWaiting = false;
    }

    private void StopNavigation() {
        isNavigating = false;
        agent.isStopped = true;
    }

    private void OnDestroy() {
        if (childNavAgent) {
            childNavAgent.OnStartNavigation -= StartNavigation;
            childNavAgent.OnEndNavigation -= StopNavigation;
        }
    }
}