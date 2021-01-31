using System;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    public static TimeSystem Instance { get; private set; }
    
    public static event Action<float> OnTimeChanged = delegate { };
    public static event Action OnTimeStarted = delegate { };
    public static event Action OnTimeEnded = delegate { };
    
    [SerializeField]
    private float time = 10;

    private float timer;
    private bool startedTime = false;
    
    public static event Action<string> OnReadableTimeChanged = delegate { };
    
    private int minutes = 0;
    private int seconds = 0;
    
    
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
        
        IntroDialog.OnIntroComplete += StartTime;
    }

    void Update()
    {
        if (startedTime && timer > 0)
        {
            timer -= Time.deltaTime;
            OnTimeChanged.Invoke(timer);
            
            SendReadableTime();
        }
        else if (startedTime)
        {
            timer = 0;
            startedTime = false;
            OnTimeEnded.Invoke();
            
            SendReadableTime();
        }
    }
    
    public float GetTimer()
    {
        return timer;
    }

    private void SendReadableTime()
    {
        minutes = Mathf.FloorToInt((timer < 0 ? 0 : timer) / 60);
        seconds = Mathf.FloorToInt((timer < 0 ? 0 : timer) % 60);
            
        OnReadableTimeChanged.Invoke(string.Format("{0:00}:{1:00}", minutes, seconds));
    }

    private void StartTime()
    {
        timer = time;
        startedTime = true;
        OnTimeStarted.Invoke();
    }
    
    private void OnDestroy()
    {
        Instance = null;
    }
}
