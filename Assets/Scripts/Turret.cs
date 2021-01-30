using System;
using System.Collections;
using System.Collections.Generic;
using ECM.Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

public class Turret : MonoBehaviour {

    public int stunDuration = 1;
    public int shootInterval = 3;
    public float ballSpeed = 5;
    [Range(1, 10)]
    public float ballSpeedRandomness = 1.01f;
    public float spread = 0.3f;
    public int despawnTime = 5;
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
            yield return new WaitForSeconds(shootInterval);
        }
    }

    private void Shoot() {
        GameObject turretBall = Instantiate(this.turretBall);
        turretBall.GetComponent<TurretBall>().canStun = true;
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