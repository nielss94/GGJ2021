using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Child : MonoBehaviour
{
    public float knockdownTime;
    public bool canGetKnockedDown = true;

    public void KnockBack(Vector3 normalizedAngle, float force)
    {
        GetComponent<Rigidbody>().AddForce(normalizedAngle * force, ForceMode.Impulse);
    }
}
