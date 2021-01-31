using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    
    private void Awake()
    {
        TimeSystem.OnReadableTimeChanged += ChangeTimeText;
    }

    private void ChangeTimeText(string time)
    {
        timerText.text = time;
    }

    private void OnDestroy()
    {
        TimeSystem.OnReadableTimeChanged -= ChangeTimeText;
    }
}
