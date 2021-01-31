using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class RandomPatrol : MonoBehaviour {
    public NavTarget[] NavTargets;
    public float WaitTime = 0f;
    
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
            agent.remainingDistance < 0.5f) {
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

        agent.destination = NavTargets[Random.Range(0, NavTargets.Length - 1)].transform.position;
        isWaiting = false;
    }

    private void StopNavigation() {
        isNavigating = false;
        agent.isStopped = true;
    }

    private void OnDestroy() {
        childNavAgent.OnStartNavigation -= StartNavigation;
        childNavAgent.OnEndNavigation -= StopNavigation;
    }
}