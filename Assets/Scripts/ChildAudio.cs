using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChildAudio : MonoBehaviour
{
    public AudioClip[] randomVoiceLines;
    public AudioClip[] onTakenVoiceLines;
    public AudioClip[] onFailedTakenVoiceLines;

    [Header("Audio frequency settings")] 
    public float minInterval;
    public float maxInterval;
    [Range(0, 100)] public int chanceToDoVoiceLinePerInterval;

    private float previousVoiceLineTryTimestamp;
    private float nextVoiceLineTryTimestamp;
    private AudioSource audioSource;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        nextVoiceLineTryTimestamp = Time.time + Random.Range(minInterval, maxInterval);
    }

    private void Update()
    {
        if (Time.time > nextVoiceLineTryTimestamp)
        {
            RandomVoiceLine();
            nextVoiceLineTryTimestamp = Time.time + Random.Range(minInterval, maxInterval);
        }
    }

    public void Taken()
    {
        audioSource.PlayOneShot(onTakenVoiceLines[Random.Range(0, onTakenVoiceLines.Length)]);
    }

    private void RandomVoiceLine()
    {
        int rand = Random.Range(0, 100);
        if (rand < chanceToDoVoiceLinePerInterval)
        {
            audioSource.PlayOneShot(randomVoiceLines[Random.Range(0, randomVoiceLines.Length)]);
        }
    }

    public void FailedTaken()
    {
        audioSource.PlayOneShot(onFailedTakenVoiceLines[Random.Range(0, onFailedTakenVoiceLines.Length)]);
    }
}
