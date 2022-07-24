using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    GameManager gm;

    TorusMovement player;

    public float sizeCoefficientBase = 0;
    //public float sizeCoefficientCutoff = 0;

    //public float spawnSpeed = 0;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<TorusMovement>();
    }

    void Update()
    {
        float sizeCoeff = 0;
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        foreach (var x in asteroids) {
            sizeCoeff += Mathf.Pow(sizeCoefficientBase, x.GetComponent<Asteroid>().size);
        }

        //Debug.Log(sizeCoeff);
        if (sizeCoeff < gm.spawnCapacity) {
            float randomMajorAngle = player.majorAngle + UnityEngine.Random.Range(0.0f, Mathf.PI * 2);
            float randomMinorAngle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);

            GameObject newAsteroid = Instantiate(gm.asteroidPrefab);
            var asteroidMovement = newAsteroid.GetComponent<TorusMovement>();

            asteroidMovement.majorRadius = player.majorRadius;
            asteroidMovement.minorRadius = player.minorRadius;

            asteroidMovement.majorAngle = randomMajorAngle;
            asteroidMovement.minorAngle = randomMinorAngle;


            Vector2 randomVelocity = Random.insideUnitCircle.normalized;
            asteroidMovement.majorAngleSpeed = randomVelocity.x * gm.spawnSpeed / asteroidMovement.majorRadius;
            asteroidMovement.minorAngleSpeed = randomVelocity.y * gm.spawnSpeed / asteroidMovement.minorRadius;

            var spawnMovement = newAsteroid.GetComponent<AsteroidMovement>();

            var linearMovement = newAsteroid.GetComponent<LinearMovement>();
            linearMovement.velocity = asteroidMovement.CartesianVelocity();

            asteroidMovement.enabled = false;

            newAsteroid.transform.position = asteroidMovement.CartesianPosition() - spawnMovement.spawnTime * linearMovement.velocity;
        }

    }

}
