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
        transform.Rotate(angle * (speed * Time.deltaTime));
    }
}
