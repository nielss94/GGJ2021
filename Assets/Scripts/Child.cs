using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Child : MonoBehaviour
{
    public bool canGetKnockedDown = true;
    public Transform hats;
    [SerializeField] private Renderer renderer;
    
    public void KnockBack(Vector3 normalizedAngle, float force)
    {
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
}
