using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LostChildSystem : MonoBehaviour
{
    public static event Action<Child> OnNewChildSelected = delegate { };
    
    [SerializeField] private List<Child> children;
    [SerializeField] private Child lostChild;

    private void Awake()
    {
        FindAllChildren();
    }

    private void Start()
    {
        GameManager.OnGameStarted += ChooseNewLostChild;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChooseNewLostChild();    
        }
    }

    private bool IsCurrentLostChild(Child child)
    {
        return child == lostChild;
    }

    private void ChooseNewLostChild()
    {
        if (lostChild)
        {
            children.Remove(lostChild);
        }

        if (children.Count == 0)
        {
            return;
        }

        lostChild = children[Random.Range(0, children.Count)];
        OnNewChildSelected?.Invoke(lostChild);
    }

    private void FindAllChildren()
    {
        children = FindObjectsOfType<Child>().ToList();
    }
}
