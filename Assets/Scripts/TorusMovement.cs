using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusMovement : MonoBehaviour
{
    public float majorRadius = 0;
    public float minorRadius = 0;
    public float majorAngle = 0;
    public float minorAngle = 0;

    public float majorAngleSpeed = 0;
    public float minorAngleSpeed = 0;

    void Update()
    {
        float dt = Time.deltaTime;
        majorAngle += dt * majorAngleSpeed;
        minorAngle += dt * minorAngleSpeed;

        if (majorAngle >= 2 * Mathf.PI) majorAngle -= 2 * Mathf.PI;
        if (majorAngle < 0) majorAngle += 2 * Mathf.PI;
        if (minorAngle >= 2 * Mathf.PI) minorAngle -= 2 * Mathf.PI;
        if (minorAngle < 0) minorAngle += 2 * Mathf.PI;

        gameObject.transform.position = CartesianPosition();
    }

    public Vector3 CartesianPosition() {
        float x = (majorRadius + minorRadius * Mathf.Cos(minorAngle)) * Mathf.Cos(majorAngle);
        float y = (majorRadius + minorRadius * Mathf.Cos(minorAngle)) * Mathf.Sin(majorAngle);
        float z = minorRadius * Mathf.Sin(minorAngle);
        return new Vector3(x, y, z);
    }

    public Vector3 CartesianVelocity() {
        float xVel = -Mathf.Sin(majorAngle) * (majorRadius + minorRadius * Mathf.Cos(minorAngle)) * majorAngleSpeed 
            + -minorRadius * Mathf.Cos(majorAngle) * Mathf.Sin(minorAngle) * minorAngleSpeed;
        float yVel = Mathf.Cos(majorAngle) * (majorRadius + minorRadius * Mathf.Cos(minorAngle)) * majorAngleSpeed 
            + -minorRadius * Mathf.Sin(majorAngle) * Mathf.Sin(minorAngle) * minorAngleSpeed;
        float zVel = minorRadius * Mathf.Cos(minorAngle) * minorAngleSpeed;
        return new Vector3(xVel, yVel, zVel);
    }

    public Vector3 NormalVector() {
        float a1 = -majorRadius * Mathf.Sin(majorAngle) - minorRadius * Mathf.Sin(majorAngle) * Mathf.Cos(minorAngle);
        float a2 = majorRadius * Mathf.Cos(majorAngle) + minorRadius * Mathf.Cos(majorAngle) * Mathf.Cos(minorAngle);
        float a3 = 0;

        float b1 = -minorRadius * Mathf.Cos(majorAngle) * Mathf.Sin(minorAngle);
        float b2 = -minorRadius * Mathf.Sin(majorAngle) * Mathf.Sin(minorAngle);
        float b3 = minorRadius * Mathf.Cos(minorAngle);

        Vector3 a = new Vector3(a1, a2, a3);
        Vector3 b = new Vector3(b1, b2, b3);

        Vector3 normal = Vector3.Cross(a, b);
        return Vector3.Normalize(normal);
    }

}
