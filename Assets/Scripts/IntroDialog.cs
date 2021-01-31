using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class IntroDialog : MonoBehaviour
{
    public static event Action OnIntroComplete = delegate {  };

    public List<Dialog> possibleDialogs;
    public Image dialogImage;
    public TextMeshProUGUI dialogText;
    
    public AudioSource mainAudioSource;
    private AudioSource audioSource;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager.OnGameStarted += ShowDialog;
    }

    private void ShowDialog()
    {
        if (possibleDialogs.Count > 0)
        {
            Dialog dialog = possibleDialogs[Random.Range(0, possibleDialogs.Count)];

            dialogImage.sprite = dialog.image;
            dialogText.text = dialog.text;

            mainAudioSource.DOFade(.5f, 1f)
                .OnComplete(() =>
                {
                    audioSource.PlayOneShot(dialog.audioClip);
                    StartCoroutine(WaitAndComplete(dialog.audioClip.length));
                });
        }
        else
        {
            OnIntroComplete?.Invoke();
        }

    }

    private IEnumerator WaitAndComplete(float t)
    {
        yield return new WaitForSeconds(t + 1);
        mainAudioSource.DOFade(1f, .5f);
        OnIntroComplete?.Invoke();
    }
}
