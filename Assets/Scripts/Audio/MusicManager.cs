using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{

    public static MusicManager Instance = null;

    enum Song { Main, Stunned, Carrying, Countdown};
    private float musicVol;
    private float stunMusicVol;

    private const float MUTED = -80.0f;


    private Song currentSong;
    private string[] mixerNames = { "MusicVolume", "StunMusicVolume", "CarryingMusicVolune", "CountdownMusicVolume" };

    [SerializeField] private AudioMixer masterMixer;


    private void Awake()
	{
		if (!Instance) Instance = this;
		else if (Instance != this) Destroy(gameObject);		
	}

	void Start()
    {
        currentSong = Song.Main;

    }

    public void ActivateStunnedMusic()
    {
        masterMixer.GetFloat(mixerNames[(int)currentSong], out musicVol);

        SetMixerVolume(mixerNames[(int)currentSong], MUTED);
        SetMixerVolume(mixerNames[(int)Song.Stunned], musicVol);
    }

    public void DeactivateStunnedMusic()
    {
        SetMixerVolume(mixerNames[(int)currentSong], musicVol);
        SetMixerVolume(mixerNames[(int)Song.Stunned], MUTED);
    }

    public void ActivateCarryingMusic()
    {
        currentSong = Song.Carrying;
    }

    public void DeactivateCarryingMusic()
    {
        currentSong = Song.Main; //let op COUNTDOWN
    }


    private void SetMixerVolume(string target, float soundLevel)
    {
        masterMixer.SetFloat(target, soundLevel);
    }


}
