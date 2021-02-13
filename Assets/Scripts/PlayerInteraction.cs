using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField] private Transform childHolder;
    [SerializeField] private KeyCode interactButton;
    private Child interactingChild = null;
    private Child takenChild = null;
    private bool canDeliver = false;
    private bool insideDeliveryPoint = false;
    
    private Ray ray;
    private void Update()
    {
        if (interactingChild && Input.GetButtonDown("Interact"))
        {
            InteractWithChild(interactingChild);
        }
        
        if (canDeliver && Input.GetButtonDown("Interact"))
        {
            if (LostChildSystem.Instance.DeliverChild(takenChild))
            {
                takenChild.gameObject.SetActive(false);
                takenChild = null;
                OnDeliverChild.Invoke();
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

        canDeliver = canDeliver || takenChild && insideDeliveryPoint;
        OnDeliverInteraction?.Invoke(canDeliver);
    }
    
    private void ChildInteraction()
    {
        if (Physics.Raycast(ray, out var hit, interactionDistance, childHitLayer) && !takenChild)
        {
            // if hit child == currentchild > return
            // if hit child != currentchild > turn off current outline, turn on new outline
            // outline on hit child
            if (hit.transform.gameObject.TryGetComponent(out Child child))
            {
                if (interactingChild)
                {
                    if (interactingChild.TryGetComponent(out Outline iOutline))
                    {
                        iOutline.enabled = false;
                    }
                }
                
                if (child.TryGetComponent(out Outline outline))
                {
                    outline.enabled = true;
                }

                interactingChild = child;
            }
        }
        else
        {
            
            if (interactingChild)
            {
                if (interactingChild.TryGetComponent(out Outline iOutline))
                {
                    iOutline.enabled = false;
                }
                
                interactingChild = null;
            }
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
            child.GetComponent<ChildAudio>().FailedTaken();
        }
    }

    private void TakeChild(Child child)
    {
        takenChild = child;
        takenChild.transform.SetParent(childHolder);
        takenChild.GetComponent<Rigidbody>().isKinematic = true;
        takenChild.GetComponent<Collider>().enabled = false;
        takenChild.GetComponent<NavMeshAgent>().enabled = false;
        takenChild.GetComponent<ChildNavAgent>().enabled = false;
        takenChild.GetComponent<Animator>().SetBool("flounder", true);
        takenChild.transform.localPosition = Vector3.zero;
        takenChild.transform.localEulerAngles = Vector3.zero;
        
        OnTakeChild?.Invoke(takenChild);
        
        takenChild.GetComponent<ChildAudio>().Taken();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ChildDelivery"))
        {
            insideDeliveryPoint = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ChildDelivery"))
        {
            insideDeliveryPoint = false;
        }
    }
}
