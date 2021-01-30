using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostChildRenderer : MonoBehaviour
{
    [SerializeField] private Transform childPos;
    private Child currentChild;

    public void NewChild(Child child)
    {
        if (currentChild)
        {
            Destroy(currentChild);
        }

        Child newChild = Instantiate(child, childPos.position, Quaternion.identity, childPos);
        Destroy(newChild.gameObject.GetComponent<Child>());
    }
}
