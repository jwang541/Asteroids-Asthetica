using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public float spawnTime = 0;
    float spawnTimer = 0;

    TorusMovement torusMovement;
    LinearMovement linearMovement;

    AsteroidCollision asteroidCollision;
    Collider asteroidCollider;

    void Start()
    {
        torusMovement = GetComponent<TorusMovement>();
        linearMovement = GetComponent<LinearMovement>();

        asteroidCollision = GetComponentInChildren<AsteroidCollision>();
        asteroidCollider = GetComponentInChildren<Collider>();
    }

    void Update()
    {
        float dt = Time.deltaTime;

        spawnTimer += dt;

        if (spawnTimer < spawnTime) {
            torusMovement.enabled = false;
            asteroidCollision.enabled = false;
            asteroidCollider.enabled = false;
            linearMovement.enabled = true;
        }
        else {
            torusMovement.enabled = true;
            asteroidCollision.enabled = true;
            asteroidCollider.enabled = true;
            linearMovement.enabled = false;
        }

    }

    public void SetSpawnTime(float time) {
        spawnTime = time;
    }
}
