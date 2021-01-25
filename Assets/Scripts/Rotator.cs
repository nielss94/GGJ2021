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
        transform.Rotate(angle * (Time.deltaTime * speed));
        transform.position = new Vector3(0, Mathf.Sin(speed), 0);
    }

    private void Start()
    {
        Awesome();
    }

    private void Awesome()
    {
        Debug.Log("Not so Awesome");
    }
}
