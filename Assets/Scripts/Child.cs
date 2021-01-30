using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class Child : MonoBehaviour
{
    public bool canGetKnockedDown = true;
    public Transform hats;
    public float stunTime = 2;
    [SerializeField] private Renderer renderer;
    
    public void KnockBack(Vector3 normalizedAngle, float force)
    {
        if (transform.TryGetComponent<NavMeshAgent>(out var agent)) {
            agent.enabled = false;
            canGetKnockedDown = false;
            StartCoroutine(StandUp());
        }
        
        GetComponent<Rigidbody>().AddForce(normalizedAngle * force, ForceMode.Impulse);

    }

    public void SetVisualCombination(KeyValuePair<Material, string> combo)
    {
        renderer.material = combo.Key;
        
        // Set hat
        if (combo.Value != "")
        {
            hats.Find(combo.Value).gameObject.SetActive(true);
        }
    }

    private IEnumerator StandUp() {
        yield return new WaitForSeconds(stunTime);

        GetComponent<NavMeshAgent>().enabled = true;
        canGetKnockedDown = true;
    }
}
