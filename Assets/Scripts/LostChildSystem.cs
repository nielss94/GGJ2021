using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LostChildSystem : MonoBehaviour
{
    public static LostChildSystem Instance { get; private set; }
    public static event Action<Child> OnNewChildSelected = delegate { };
    
    [SerializeField] private List<Child> children;
    [SerializeField] private Child lostChild;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        FindAllChildren();
    }

    private void Start()
    {
        GameManager.OnGameStarted += ChooseNewLostChild;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChooseNewLostChild();    
        }
    }

    public bool DeliverChild(Child child)
    {
        if (!IsCurrentLostChild(child))
        {
            return false;
        }
        
        ChooseNewLostChild();   
        PointAwardSystem.Instance.DoAwardPoints();
        
        return true;
    }

    public bool IsCurrentLostChild(Child child)
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