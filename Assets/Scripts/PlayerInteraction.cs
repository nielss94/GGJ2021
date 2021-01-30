using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static event Action<Child> OnInteraction = delegate { };
    public static event Action<Child> OnTakeChild = delegate { };
    public static event Action OnFailTakeChild = delegate { };
    public static event Action OnDeliverChild = delegate { };
    public static event Action<bool> OnDeliverInteraction = delegate { };
    [SerializeField] private Transform interactionCamera;
    [SerializeField] private LayerMask childHitLayer;
    [SerializeField] private LayerMask deliveryHitLayer;
    [SerializeField] private float interactionDistance;

    [SerializeField] private KeyCode interactButton;

    private Child interactingChild = null;
    private Child takenChild = null;
    private bool canDeliver = false;

    private Ray ray;
    private void Update()
    {
        if (interactingChild && Input.GetKeyDown(interactButton))
        {
            InteractWithChild(interactingChild);
        }
        
        if (canDeliver && Input.GetKeyDown(interactButton))
        {
            if (LostChildSystem.Instance.DeliverChild(takenChild))
            {
                takenChild = null;
            }
        }
    }

    private void FixedUpdate()
    {
        ray = new Ray(interactionCamera.position, interactionCamera.TransformDirection(Vector3.forward));
        ChildInteraction();
        DeliveryInteraction();
    }

    private void DeliveryInteraction()
    {
        if (Physics.Raycast(ray, interactionDistance, deliveryHitLayer) && takenChild)
        {
            canDeliver = true;
        }
        else
        {
            canDeliver = false;
        }

        OnDeliverInteraction?.Invoke(canDeliver);
    }
    
    private void ChildInteraction()
    {
        if (Physics.Raycast(ray, out var hit, interactionDistance, childHitLayer) && !takenChild)
        {
            if (hit.transform.gameObject.TryGetComponent(out Child child))
            {
                interactingChild = child;
            }
            else
            {
                interactingChild = null;
            }
        }
        else
        {
            interactingChild = null;
        }
        
        OnInteraction?.Invoke(interactingChild);
    }

    private void InteractWithChild(Child child)
    {
        if (LostChildSystem.Instance.IsCurrentLostChild(child))
        {
            TakeChild(child);
        }
        else
        {
            OnFailTakeChild?.Invoke();
        }
    }

    private void TakeChild(Child child)
    {
        takenChild = child;
        takenChild.gameObject.SetActive(false);
        
        OnTakeChild?.Invoke(takenChild);
    }

}