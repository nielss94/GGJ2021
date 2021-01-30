using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public class RandomMaterial : MonoBehaviour
{
    public Material[] materials;

    private Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        ApplyRandomMaterial();
    }

    private void ApplyRandomMaterial()
    {
        if (materials.Length > 0)
        {
            renderer.material = materials[Random.Range(0, materials.Length)];
        }    
    }
}
