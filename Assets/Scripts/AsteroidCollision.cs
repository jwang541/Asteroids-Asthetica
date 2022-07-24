using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour
{
    GameManager gm;

    public string triggerTag;
    Asteroid asteroid;

    TorusMovement movement;

    void Start() {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        movement = GetComponentInParent<TorusMovement>();
        asteroid = GetComponentInParent<Asteroid>();
    }

    void OnCollisionEnter(Collision collision) 
    {
        if (collision.collider.tag == triggerTag) {
            Destroy(collision.collider.gameObject);
            if (asteroid.size > 0) {
                for (int i = 0; i < asteroid.splitCount; i++) {

                    GameObject newAsteroid = Instantiate(gm.asteroidPrefab, transform.parent.position, Quaternion.identity);
                    newAsteroid.transform.localScale = 0.5f * transform.parent.localScale;

                    var newAsteroidData = newAsteroid.GetComponent<Asteroid>();
                    newAsteroidData.size = asteroid.size - 1;

                    var asteroidMovement = newAsteroid.GetComponent<TorusMovement>();
                    asteroidMovement.majorRadius = movement.majorRadius;
                    asteroidMovement.minorRadius = movement.minorRadius;

                    asteroidMovement.majorAngle = movement.majorAngle;
                    asteroidMovement.minorAngle = movement.minorAngle;

                    Vector2 randomDeltaVelocity = Random.insideUnitCircle.normalized;

                    asteroidMovement.majorAngleSpeed = movement.majorAngleSpeed + randomDeltaVelocity.x * gm.splitSpeed;
                    asteroidMovement.minorAngleSpeed = movement.minorAngleSpeed + randomDeltaVelocity.y * gm.splitSpeed;

                    var asteroidSpawnMovement = newAsteroid.GetComponent<AsteroidMovement>();
                    asteroidSpawnMovement.SetSpawnTime(0);
                }

            }

            switch (asteroid.size) {
                case 0:
                    gm.score += 10;
                    break;
                case 1:
                    gm.score += 5;
                    break;
                case 2: 
                    gm.score += 2;
                    break;
                default:
                    gm.score += 1;
                    break;
            }

            GameObject explosion = Instantiate(gm.asteroidExplosionPrefab, transform.parent.position, Random.rotation);
            explosion.transform.localScale = Mathf.Pow(2, asteroid.size) * explosion.transform.localScale;
            explosion.transform.localScale = 0.1f * explosion.transform.localScale;

            gm.asteroidExplosionSound.Play();

            Destroy(transform.parent.gameObject);
        }
    }

}
