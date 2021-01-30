using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAwardSystem : MonoBehaviour
{
    public static PointAwardSystem Instance { get; private set; }

    [SerializeField]
    private int standardPoints = 5;

    [SerializeField] private float shortTime = 15;
    [SerializeField] private float mediumTime = 30;
    
    public static event Action OnShort = delegate { };
    public static event Action OnMedium = delegate { };
    public static event Action OnLong = delegate { };
    
    [SerializeField]
    private int shortPoints = 10;
    [SerializeField]
    private int mediumPoints = 5;
    [SerializeField]
    private int longPoints = 0;
    
    private TimeSystem timeSystem;
    private PointSystem pointSystem;

    private float startTime = 0;
    private float endTime = 0;

    private bool canAward = false;
    
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

        TimeSystem.OnTimeStarted += () => canAward = true;
        TimeSystem.OnTimeEnded += () => canAward = false;
    }

    private void Start()
    {
        pointSystem = PointSystem.Instance;
        timeSystem = TimeSystem.Instance;

        // Make sure timeSystem has started timer
        StartCoroutine(GetStartTime());
    }
    
    private IEnumerator GetStartTime()
    {
        yield return 0;
        
        startTime = timeSystem.GetTimer();
    }

    public void DoAwardPoints()
    {
        if (!canAward)
        {
            return;
        }
        
        endTime = timeSystem.GetTimer();

        float timeItTook = startTime - endTime;

        int extraPoints = 0;
        if (timeItTook <= shortTime)
        {
            extraPoints = shortPoints;
            OnShort.Invoke();
        }
        else if(timeItTook <= mediumTime)
        {
            extraPoints = mediumPoints;
            OnMedium.Invoke();
        }
        else
        {
            extraPoints = longPoints;
            OnLong.Invoke();
        }
        
        pointSystem.AddPoints(standardPoints + extraPoints);

        // Restart time
        startTime = timeSystem.GetTimer();
    }
    
}
