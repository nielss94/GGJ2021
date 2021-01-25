using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 angle;
    public float speed;
    private void Update()
    {
        // Do rotate some more
        transform.Rotate(angle * (Time.deltaTime * speed * 20));
    }

    private void Start()
    {
        Awesome();
    }

    private void Awesome()
    {
        Debug.Log("Awesome");
    }
}
