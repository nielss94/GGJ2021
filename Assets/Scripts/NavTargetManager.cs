using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NavTargetManager : MonoBehaviour {

    public List<NavTarget> NavTargets;
    public static NavTargetManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }
        
        // GatherAllNavTargets();
    }

    public List<NavTarget> GetAllNavTargets() {
        return new List<NavTarget>(NavTargets);
    }

    private void GatherAllNavTargets() {
        // NavTargets = FindObjectsOfType<NavTarget>().ToList();
        NavTargets = GetComponentsInChildren<NavTarget>().ToList();
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
