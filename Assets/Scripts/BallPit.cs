using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<PlayerSlow>(out var player)) {
            player.EnableSlow();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.TryGetComponent<PlayerSlow>(out var player)) {
            player.DisableSlow();
        }
    }
}
