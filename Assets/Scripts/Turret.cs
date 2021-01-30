using System;
using System.Collections;
using System.Collections.Generic;
using ECM.Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

public class Turret : MonoBehaviour {

    public int stunDuration = 1;
    public int shootDelay = 1;
    public float ballSpeed = 10;
    [Range(1, 10)]
    public float ballSpeedRandomness = 1.01f;
    public float spread = 0.01f;
    public int despawnTime = 10;
    public GameObject turretBall;
    public Transform ballSpawnPoint;

    private bool active = false;
    private void Start() {
        active = true;
        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine() {
        while (active) {
            Shoot();
            yield return new WaitForSeconds(shootDelay);
        }
    }

    private void Shoot() {
        GameObject turretBall = Instantiate(this.turretBall);
        turretBall.transform.position = ballSpawnPoint.position;
        float randomBallSpeed = ballSpeed * Random.Range(1f, ballSpeedRandomness);
        Vector3 randomSpread = ballSpawnPoint.right * Random.Range(-spread, spread);
        Vector3 heading = ballSpawnPoint.forward * randomBallSpeed;
        turretBall.GetComponent<Rigidbody>().AddForce(heading + randomSpread, ForceMode.Impulse);
        turretBall.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        StartCoroutine(DestroyBall(turretBall));
    }

    private IEnumerator DestroyBall(GameObject ball) {
        yield return new WaitForSeconds(despawnTime);
        Destroy(ball);
    }
}
