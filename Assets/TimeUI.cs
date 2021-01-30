using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Dependecies")]
    [SerializeField]
    private TimeSystem timeSystem;

    private void Start()
    {
        timeSystem.OnReadableTimeChanged += ChangeTimeText;
    }

    private void ChangeTimeText(string time)
    {
        timerText.text = time;
    }
}
