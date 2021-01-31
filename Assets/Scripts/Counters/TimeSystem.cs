using System;
using UnityEngine;
using DG.Tweening;

public class TimeSystem : MonoBehaviour
{
    public static TimeSystem Instance { get; private set; }
    
    public static event Action<float> OnTimeChanged = delegate { };
    public static event Action OnTimeStarted = delegate { };
    public static event Action OnTimeEnded = delegate { };

    public AudioSource mainMusic;

    [SerializeField]
    private float time = 10;
    [SerializeField]
    private AudioSource countdown;


    private float timer;
    private bool startedTime = false;
    
    public static event Action<string> OnReadableTimeChanged = delegate { };
    
    private int minutes = 0;
    private int seconds = 0;

    private bool countdownStarted = false;
    
    
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
        
        if(!countdownStarted && seconds > 25 && seconds < 30)
        {
            StartCountDown();
        }

        OnReadableTimeChanged.Invoke(string.Format("{0:00}:{1:00}", minutes, seconds));
    }

    void StartCountDown()
    {
        countdownStarted = true;
        mainMusic.DOFade(0, 0.5f);
        countdown.Play(0);
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
