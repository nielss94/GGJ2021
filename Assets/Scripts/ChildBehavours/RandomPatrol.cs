using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class RandomPatrol : MonoBehaviour {
    private NavTarget[] navTargets;
    private NavTargetManager navTargetManager;
    private NavMeshAgent agent;
    private ChildNavAgent childNavAgent;
    private bool isNavigating = false;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        childNavAgent = GetComponent<ChildNavAgent>();
        childNavAgent.OnStartNavigation += StartNavigation;
        childNavAgent.OnEndNavigation += StopNavigation;
    }

    private void StartNavigation() {
        navTargetManager = NavTargetManager.Instance;
        navTargets = navTargetManager.GetAllNavTargets();
        isNavigating = true;
    }

    private void Update() {
        if (isNavigating &&
            agent.enabled &&
            !agent.pathPending &&
            agent.remainingDistance < 0.5f) {
            GoToRandomPoint();
        }
    }

    private void GoToRandomPoint() {
        if (navTargets.Length == 0) 
            return;

        agent.destination = navTargets[Random.Range(0, navTargets.Length)].transform.position;
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