using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class RandomCrying : MonoBehaviour
{
    public List<NavTarget> NavTargets;
    public float WaitTime = 0f;
    public float approachThreshold = 0.5f;
    
    private NavTargetManager navTargetManager;
    private int destIndex;
    private NavMeshAgent agent;
    private ChildNavAgent childNavAgent;
    private Animator animator;
    private bool isNavigating = false;
    private bool isWaiting = false;
    
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        childNavAgent = GetComponent<ChildNavAgent>();
        animator = GetComponent<Animator>();
        childNavAgent.OnStartNavigation += StartNavigation;
        childNavAgent.OnEndNavigation += StopNavigation;
    }

    private void StartNavigation() {
        if (NavTargets.Count == 0) {
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
            agent.remainingDistance < approachThreshold) {
            StartCoroutine(GoToNextPoint());
        }
    }

    private IEnumerator GoToNextPoint() {
        isWaiting = true;
        animator.SetBool("crying", true);
        yield return new WaitForSeconds(WaitTime);
        if (NavTargets.Count == 0) {
            isWaiting = false;
            yield break;
        }

        animator.SetBool("crying", false);
        if (NavTargets.Count > 0) {
            var navTargetIndex = Random.Range(0, NavTargets.Count - 1);
            if (agent.enabled) agent.destination = NavTargets[navTargetIndex].transform.position;
        } else {
            Debug.LogWarning("Agent is missing nav-targets.");
        }
        
        isWaiting = false;
    }

    private void OnDestroy() {
        if (childNavAgent) {
            childNavAgent.OnStartNavigation -= StartNavigation;
            childNavAgent.OnEndNavigation -= StopNavigation;
        }
    }
}