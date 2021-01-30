using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LostChildRenderer : MonoBehaviour
{
    [SerializeField] private Transform childPos;
    [SerializeField] private GameObject currentChild;

    public void NewChild(Child child)
    {
        if (currentChild)
        {
            Destroy(currentChild);
        }

        currentChild = Instantiate(child, childPos.position, Quaternion.identity, childPos).gameObject;
        Destroy(currentChild.gameObject.GetComponent<Child>());
    }
}
