using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NoMoreAudioClipsException : Exception
{
    public NoMoreAudioClipsException() : base() { }
}

public class AudioClipTimer
{
    public AudioClip AudioClip { get; private set; }
    public float Timer { get; private set; }

    public AudioClipTimer(AudioClip audioClip, float timer)
    {
        AudioClip = audioClip;
        Timer = timer;
    }
}

public class AudiopoolSystem : MonoBehaviour
{
    public static AudiopoolSystem Instance { get; private set; }
    
    public AudioClip[] randomVoiceLines;
    public float randomVoiceLineCooldownTime;

    private List<AudioClip> randomVoiceLinesPool = new List<AudioClip>();
    private List<AudioClipTimer> usedRandomVoiceLines = new List<AudioClipTimer>();
    
    
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
        
        randomVoiceLinesPool.AddRange(randomVoiceLines);
    }

    private void Update()
    {
        UpdateRandomVoiceLinePool();
    }

    private void UpdateRandomVoiceLinePool()
    {
        for (int usedIndex = 0; usedIndex < usedRandomVoiceLines.Count; usedIndex++)
        {
            AudioClipTimer audioClipTimer = usedRandomVoiceLines[usedIndex];
            
            if (UsedRandomAudioClipCanBeAdded(audioClipTimer))
            {
                ReaddRandomVoiceLine(usedIndex, audioClipTimer.AudioClip);
            }
        }
    }

    private bool UsedRandomAudioClipCanBeAdded(AudioClipTimer audioClipTimer)
    {
        return Time.time >= audioClipTimer.Timer + randomVoiceLineCooldownTime;
    }

    private void ReaddRandomVoiceLine(int index, AudioClip audioClip)
    {
        usedRandomVoiceLines.RemoveAt(index);
        randomVoiceLinesPool.Add(audioClip);
    }

    public AudioClip GetRandomVoiceLine()
    {
        if (randomVoiceLinesPool.Count == 0)
        {
            throw new NoMoreAudioClipsException();
        }
        
        AudioClip audioClip = randomVoiceLinesPool[Random.Range(0, randomVoiceLinesPool.Count)];

        randomVoiceLinesPool.Remove(audioClip);
        usedRandomVoiceLines.Add(new AudioClipTimer(audioClip, Time.time));
        
        
        return audioClip;
    }
}
