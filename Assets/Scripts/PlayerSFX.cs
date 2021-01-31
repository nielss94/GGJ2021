using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public AudioClip dash;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        PlayerDash.OnDash += PlayDashSound;
    }

    private void PlayDashSound()
    {
        audioSource.PlayOneShot(dash);
    }

    private void OnDestroy()
    {
        PlayerDash.OnDash -= PlayDashSound;
    }
}
