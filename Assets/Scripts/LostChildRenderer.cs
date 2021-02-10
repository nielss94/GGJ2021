using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class LostChildRenderer : MonoBehaviour
{
    [SerializeField] private Transform childPos;
    [SerializeField] private GameObject currentChild;

    public void NewChild(Child child)
    {
        currentChild = Instantiate(child, childPos.position, childPos.rotation, childPos).gameObject;
        Destroy(currentChild.gameObject.GetComponent<Child>());
        Destroy(currentChild.gameObject.GetComponent<Rigidbody>());
        
        if (currentChild.TryGetComponent<BallThrower>(out var comp1)) Destroy(comp1);
        if (currentChild.TryGetComponent<BallThrowPlayer>(out var comp2)) Destroy(comp2);
        if (currentChild.TryGetComponent<OrderedPatrol>(out var comp3)) Destroy(comp3);
        if (currentChild.TryGetComponent<RandomCrying>(out var comp4)) Destroy(comp4);
        if (currentChild.TryGetComponent<RandomPatrol>(out var comp5)) Destroy(comp5);
        Destroy(currentChild.gameObject.GetComponent<NavMeshAgent>());
        Destroy(currentChild.gameObject.GetComponent<ChildNavAgent>());
    }

    public void RemoveChild()
    {
        if (currentChild)
        {
            Destroy(currentChild);
        }
    }
}
