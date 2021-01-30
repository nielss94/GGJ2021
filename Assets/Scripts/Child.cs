using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    public void KnockBack(Vector3 normalizedAngle, float force)
    {
        GetComponent<Rigidbody>().AddForce(normalizedAngle * force, ForceMode.Impulse);
    }
}
