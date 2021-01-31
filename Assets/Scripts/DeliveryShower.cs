using System;
using DG.Tweening;
using UnityEngine;

public class DeliveryShower : MonoBehaviour
{
    [SerializeField] private UIObjective[] objectiveMarkers;
    
    private void Awake()
    {
        PlayerInteraction.OnTakeChild += ShowObjectives;
        PlayerInteraction.OnDeliverChild += HideObjectives;
    }

    private void ShowObjectives(Child obj)
    {
        foreach (var objectiveMarker in objectiveMarkers)
        {
            objectiveMarker.DoShow();
           objectiveMarker.FadeInObjective();
        }
    }

    private void HideObjectives()
    {
        foreach (var objectiveMarker in objectiveMarkers)
        {
            objectiveMarker.FadeOutObjective(true);
        }
    }

    private void OnDestroy()
    {
        PlayerInteraction.OnTakeChild -= ShowObjectives;
        PlayerInteraction.OnDeliverChild -= HideObjectives;
    }
}