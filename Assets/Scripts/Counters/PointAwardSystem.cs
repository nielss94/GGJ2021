using System;
using System.Collections;
using UnityEngine;

public class PointAwardSystem : MonoBehaviour
{
    public static PointAwardSystem Instance { get; private set; }

    [SerializeField]
    private int standardPoints = 5;

    [SerializeField] private float shortTime = 15;
    [SerializeField] private float mediumTime = 30;
    
    public static event Action<int> OnShort = delegate { };
    public static event Action<int> OnMedium = delegate { };
    public static event Action<int> OnLong = delegate { };
    
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

        TimeSystem.OnTimeStarted += SetAwardTrue;
        TimeSystem.OnTimeEnded += SetAwardFalse;
    }
    

    private void Start()
    {
        pointSystem = PointSystem.Instance;
        timeSystem = TimeSystem.Instance;

        // Make sure timeSystem has started timer
        StartCoroutine(GetStartTime());
    }

    private void SetAwardTrue()
    {
        canAward = true;
    }
    
    private void SetAwardFalse()
    {
        canAward = false;
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
            OnShort.Invoke(standardPoints + extraPoints);
        }
        else if(timeItTook <= mediumTime)
        {
            extraPoints = mediumPoints;
            OnMedium.Invoke(standardPoints + extraPoints);
        }
        else
        {
            extraPoints = longPoints;
            OnLong.Invoke(standardPoints + extraPoints);
        }
        
        pointSystem.AddPoints(standardPoints + extraPoints);

        // Restart time
        startTime = timeSystem.GetTimer();
    }
    
    private void OnDestroy()
    {
        Instance = null;
        TimeSystem.OnTimeStarted -= SetAwardTrue;
        TimeSystem.OnTimeEnded -= SetAwardFalse;
    }
}
