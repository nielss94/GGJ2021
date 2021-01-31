using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Turret))]
public class BallThrowPlayer : MonoBehaviour
{
    public float ThrowTime = 3f;
    public NavTarget[] NavTargets;

    private NavTargetManager navTargetManager;
    private NavMeshAgent agent;
    private ChildNavAgent childNavAgent;
    private Turret turret;
    private bool isNavigating = false;
    private bool isThrowing = false;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        childNavAgent = GetComponent<ChildNavAgent>();
        turret = GetComponent<Turret>();
        turret.enabled = false;
        childNavAgent.OnStartNavigation += StartNavigation;
        childNavAgent.OnEndNavigation += StopNavigation;
        StartCoroutine(GoToThrowingPoint());
    }
    
    private void StartNavigation() {
        if (NavTargets.Length == 0) {
            navTargetManager = NavTargetManager.Instance;
            NavTargets = navTargetManager.GetAllNavTargets();
        }
        isNavigating = true;
    }

    private void Update() {
        if (!isThrowing &&
            isNavigating &&
            agent.enabled &&
            !agent.pathPending &&
            agent.remainingDistance < 0.5f) {
            StartCoroutine(GoToThrowingPoint());
        }
    }

    private IEnumerator GoToThrowingPoint() {
        isThrowing = true;

        float rotateTime = 0.3f;
        Player player = FindObjectOfType<Player>();
        if (player) {
            transform.DOLookAt(new Vector3(
                player.transform.position.x, 
                transform.position.y, 
                player.transform.position.z), rotateTime);
        }
        yield return new WaitForSeconds(rotateTime);

        turret.enabled = true;
        yield return new WaitForSeconds(ThrowTime);
        turret.shooting = false;
        turret.enabled = false;
        isThrowing = false;
        
        agent.destination = NavTargets[Random.Range(0, NavTargets.Length - 1)].transform.position;
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
