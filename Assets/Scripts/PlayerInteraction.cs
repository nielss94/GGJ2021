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

    [SerializeField]
    private AudioClip deliverySfx;
    private AudioSource audioSource;

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
                Destroy(takenChild.gameObject);
                takenChild = null;
                OnDeliverChild.Invoke();

                if(deliverySfx) audioSource.PlayOneShot(deliverySfx);
                MusicManager.Instance.DeactivateCarryingMusic();
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

            MusicManager.Instance.ActivateCarryingMusic();
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

        takenChild.StopStandupCoroutine();
        
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
