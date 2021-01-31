using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavTargetManager : MonoBehaviour {

    public NavTarget[] NavTargets;
    public static NavTargetManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }
    }

    private void Start() {
        GatherAllNavTargets();
    }

    public NavTarget[] GetAllNavTargets() {
        return NavTargets;
    }

    private void GatherAllNavTargets() {
        NavTargets = FindObjectsOfType<NavTarget>();
    }
}
