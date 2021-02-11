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
        PlayerDash.OnDashHit += PlayDashHitSound;
    }

    private void PlayDashSound()
    {
        audioSource.PlayOneShot(dash);
    }

    private void PlayDashHitSound() {
       // audioSource.PlayOneShot(dashHit);
    }

    private void OnDestroy()
    {
        PlayerDash.OnDash -= PlayDashSound;
    }
}
