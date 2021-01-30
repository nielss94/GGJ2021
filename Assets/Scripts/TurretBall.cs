using System.Collections;
using System.Collections.Generic;
using ECM.Controllers;
using UnityEngine;

public class TurretBall : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if (other.transform.TryGetComponent<Player>(out var player)) {
            StartCoroutine(StunPlayer(player));
        }
    }

    private IEnumerator StunPlayer(Player player) {
        // TODO: Stun in player script?
        player.GetComponent<BaseFirstPersonController>().pause = true;
        yield return new WaitForSeconds(1);
        player.GetComponent<BaseFirstPersonController>().pause = false;
    }
}
