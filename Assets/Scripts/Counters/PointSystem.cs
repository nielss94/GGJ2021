using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    [SerializeField] private bool resetPointsOnAwake = true;
    
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

        if (resetPointsOnAwake)
        {
            PlayerPrefs.SetInt("InGamePoints", 0);
        }
        
        StartCoroutine(SetPoints());
    }
    
    private IEnumerator SetPoints()
    {
        yield return 0;
        
        points = PlayerPrefs.GetInt("InGamePoints") | 0;
        OnPointsChanged.Invoke(points);
    }

    public void AddPoints(int amount)
    {
        points += amount;
        OnPointsChanged.Invoke(points);
        // return points;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("points", points);
    }
}
