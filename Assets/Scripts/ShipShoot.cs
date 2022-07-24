using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShoot : MonoBehaviour
{
    GameManager gm;

    public float shootTime = 0;
    float shootTimer = 0;

    public GameObject bullet;

    TorusMovement movement;

    public float bulletSpeed;

    void Start() {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        movement = GetComponent<TorusMovement>();
    }

    void Update()
    {
        float dt = Time.deltaTime;
        shootTimer += dt;

        if (Input.GetKey(KeyCode.Space)) {
            if (shootTimer >= shootTime) {
                shootTimer = 0;
                CreateBullet();

                gm.shipShootSound.Play();
            }
        }

    }

    void CreateBullet() {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        var bulletMovement = newBullet.GetComponent<BasicMovement>();
        bulletMovement.majorRadius = movement.majorRadius;
        bulletMovement.minorRadius = movement.minorRadius;

        bulletMovement.majorAngle = movement.majorAngle;
        bulletMovement.minorAngle = movement.minorAngle;

        float majorRadius = movement.majorRadius;
        float minorRadius = movement.minorRadius;

        float majorAngle = movement.majorAngle;
        float minorAngle = movement.minorAngle;

        Quaternion rotation = transform.rotation;
        Vector3 forward = gameObject.transform.forward;

        float a1 = -majorRadius * Mathf.Sin(majorAngle) - minorRadius * Mathf.Sin(majorAngle) * Mathf.Cos(minorAngle);
        float a2 = majorRadius * Mathf.Cos(majorAngle) + minorRadius * Mathf.Cos(majorAngle) * Mathf.Cos(minorAngle);
        float a3 = 0;

        float b1 = -minorRadius * Mathf.Cos(majorAngle) * Mathf.Sin(minorAngle);
        float b2 = -minorRadius * Mathf.Sin(majorAngle) * Mathf.Sin(minorAngle);
        float b3 = minorRadius * Mathf.Cos(minorAngle);

        Vector3 a = Vector3.Normalize(new Vector3(a1, a2, a3));
        Vector3 b = Vector3.Normalize(new Vector3(b1, b2, b3));

        float majorShootComponent = Vector3.Dot(a, forward);  
        float minorShootComponent = Vector3.Dot(b, forward);

        bulletMovement.majorAngleSpeed = movement.majorAngleSpeed + majorShootComponent * bulletSpeed / majorRadius;
        bulletMovement.minorAngleSpeed = movement.minorAngleSpeed + minorShootComponent * bulletSpeed / minorRadius;
    }




}
