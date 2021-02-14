using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ECM.Controllers;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    public static event Action<bool> OnSetStunned = delegate {  };
    
    public GameObject stunPivot;
    public float stunDuration = 1f;
    public float stunRotationSpeed = 100f;
    public float coolDownTime = 2f;
    
    private Vector3 initialStunPivotPos;
    private bool isStunned = false;
    private bool isCooldown = false;

    [SerializeField]
    private AudioClip stunSfx;
    private AudioSource audioSource;



private void Start() {
        initialStunPivotPos = stunPivot.transform.localPosition;
    }

    private void Update() {
        if (isStunned) {
            stunPivot.transform.Rotate(new Vector3(0, -1, 0) * (stunRotationSpeed * Time.deltaTime));
        }
    }

    public IEnumerator CooldownStun()
    {
        isCooldown = true;
        
        yield return new WaitForSeconds(coolDownTime);

        isCooldown = false;


        MusicManager.Instance.DeactivateStunnedMusic();

    }
    
    public void Stun() {

        if (isCooldown)
        {
            return;
        }

        StartCoroutine(WaitStun());
        StartCoroutine(CooldownStun());

        PlayStunSound();
    }

    public IEnumerator WaitStun()
    {
        isStunned = true;
        OnSetStunned.Invoke(isStunned);
        
        stunPivot.SetActive(true);
        stunPivot.transform.localPosition += transform.up * 0.5f;
        Vector3 raisedStunPos = stunPivot.transform.localPosition;
        stunPivot.transform.DOLocalMove(initialStunPivotPos, 0.2f);
        
        // Small wait to let player get pushed back
        yield return new WaitForSeconds(0.2f);

        BaseFirstPersonController fpController = GetComponent<BaseFirstPersonController>();
        fpController.GetComponent<BaseFirstPersonController>().pause = true;
        yield return new WaitForSeconds(stunDuration);
        fpController.GetComponent<BaseFirstPersonController>().pause = false;
        
        stunPivot.transform.DOLocalMove(raisedStunPos, 0.2f).OnComplete(() => {
            stunPivot.SetActive(false);
        });
        isStunned = false;
        OnSetStunned.Invoke(isStunned);
    }

    private void PlayStunSound()
    {
        if(!audioSource) audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(stunSfx);

        MusicManager.Instance.ActivateStunnedMusic();
    }
}
