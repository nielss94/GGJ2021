using System.Collections;
using System.Collections.Generic;
using ECM.Controllers;
using UnityEngine;

public class TurretBall : MonoBehaviour {
    public bool canStun = false;

    private void OnCollisionEnter(Collision other) {
        if (other.transform.TryGetComponent<PlayerStun>(out var player)) {
            if (canStun)
            {
                player.Stun();
            }
        }
    }
}
