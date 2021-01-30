using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public static PointSystem Instance { get; private set; }
    
    public static event Action<int> OnPointsChanged = delegate { };
    
    private int points = 0;
    
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
    }

    public void AddPoints(int amount)
    {
        points += amount;
        OnPointsChanged.Invoke(points);
        // return points;
    }
}
