using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavTargetManager : MonoBehaviour {

    private NavTarget[] NavTargets;
    public static NavTargetManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }
        
        GatherAllNavTargets();
    }

    public NavTarget[] GetAllNavTargets() {
        return NavTargets;
    }

    private void GatherAllNavTargets() {
        NavTargets = FindObjectsOfType<NavTarget>();
    }
    
    private void OnDestroy()
    {
        Instance = null;
    }
}
