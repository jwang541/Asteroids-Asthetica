using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : TorusMovement
{
    GameManager gm;

    public float localRotation = 0;
    public float localRotationSpeed = 0;

    public float acceleration = 0;
    public float drag = 0;

    float volume = 0;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();  
    }

    void Update()
    {
        float dt = Time.deltaTime;
        UpdatePosition(dt);

        if (majorAngle >= 2 * Mathf.PI) majorAngle -= 2 * Mathf.PI;
        if (majorAngle < 0) majorAngle += 2 * Mathf.PI;
        if (minorAngle >= 2 * Mathf.PI) minorAngle -= 2 * Mathf.PI;
        if (minorAngle < 0) minorAngle += 2 * Mathf.PI;

        gameObject.transform.position = CartesianPosition();

        if (Input.GetKey(KeyCode.A)) localRotation -= dt * localRotationSpeed;
        if (Input.GetKey(KeyCode.D)) localRotation += dt * localRotationSpeed;

        if (Input.GetKey(KeyCode.W)) Accelerate(dt, 1.0f);

        if (!Input.GetKey(KeyCode.W)) volume /= Mathf.Pow(10, dt);
        else volume = Mathf.Min(0.2f, Mathf.Max(0.01f, volume * Mathf.Pow(10, dt)));
        gm.shipAccelerateSound.volume = volume;

        if (localRotation >= 360) localRotation -= 360;
        if (localRotation < 0) localRotation += 360;
        ApplyLocalRotation();

        ApplyDrag(dt);

    }

    void ApplyLocalRotation() {
        float b1 = -minorRadius * Mathf.Cos(majorAngle) * Mathf.Sin(minorAngle);
        float b2 = -minorRadius * Mathf.Sin(majorAngle) * Mathf.Sin(minorAngle);
        float b3 = minorRadius * Mathf.Cos(minorAngle);

        Vector3 b = new Vector3(b1, b2, b3);
        b = Vector3.Normalize(b);

        Vector3 norm = NormalVector();

        Vector3 rotated = Quaternion.AngleAxis(localRotation, norm) * b;
        Quaternion rotation = Quaternion.LookRotation(rotated, norm);
        transform.rotation = rotation;
    }

    void Accelerate(float dt, float scale) {
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

        float majorAccelerationComponent = Vector3.Dot(a, forward);  
        float minorAccelerationComponent = Vector3.Dot(b, forward);

        majorAngleSpeed += dt * majorAccelerationComponent * acceleration / majorRadius;
        minorAngleSpeed += dt * minorAccelerationComponent * acceleration / minorRadius;
    }

    void ApplyDrag(float dt) {
        majorAngleSpeed *= Mathf.Pow(drag, dt);
        minorAngleSpeed *= Mathf.Pow(drag, dt);
    }

    void UpdatePosition(float dt) {
        majorAngle += dt * majorAngleSpeed;
        minorAngle += dt * minorAngleSpeed;
        /*
        Vector3 vel = CartesianVelocity();
        Vector3 pos = CartesianPosition();

        float speed = Vector3.Magnitude(vel);

        Vector3 posf = pos + vel * dt;
        
        float newMajorAngle = Mathf.Atan2(posf.y, posf.x);
        Vector3 newMajorPosition = new Vector3(majorRadius * Mathf.Cos(newMajorAngle), majorRadius * Mathf.Sin(newMajorAngle), 0);

        float newMinorAngle = Mathf.Atan2(posf.z, Vector3.Distance(newMajorPosition, posf));*/
        //Debug.Log("new major: " + newMajorAngle);
        //Debug.Log("new minor: " + newMinorAngle);
        //Debug.Log("initial pos: " + pos.ToString("F4"));
        //Debug.Log("final pos: " + posf.ToString("F4"));
        //Debug.Log("vel: " + vel.ToString("F4"));
        //Debug.Log("speed: " + speed);
        //Debug.Log("---------");

        //majorAngle = newMajorAngle;
        //minorAngle = newMinorAngle;



    }

}
