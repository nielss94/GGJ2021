using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ECM.Controllers;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    public GameObject stunPivot;
    public int stunDuration = 1;
    public float stunRotationSpeed = 100f;
    
    private Vector3 initialStunPivotPos;
    private bool isStunned = false;

    private void Start() {
        initialStunPivotPos = stunPivot.transform.localPosition;
        Debug.Log(initialStunPivotPos);
    }

    private void Update() {
        if (isStunned) {
            stunPivot.transform.Rotate(new Vector3(0, -1, 0) * (stunRotationSpeed * Time.deltaTime));
        }
    }
    
    public IEnumerator Stun() {
        yield return new WaitForSeconds(0.2f);
        isStunned = true;
        stunPivot.transform.localPosition += transform.up * 0.5f;
        stunPivot.transform.DOLocalMove(initialStunPivotPos, 0.4f);
        BaseFirstPersonController fpController = GetComponent<BaseFirstPersonController>();
        stunPivot.SetActive(true);
        fpController.GetComponent<BaseFirstPersonController>().pause = true;
        yield return new WaitForSeconds(stunDuration);
        stunPivot.SetActive(false);
        fpController.GetComponent<BaseFirstPersonController>().pause = false;
        isStunned = false;
    }
}
