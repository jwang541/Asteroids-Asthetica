using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : TorusMovement
{
    public float localRotation = 0;
    public float localRotationSpeed = 0;

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

        if (localRotation >= 360) localRotation -= 360;
        if (localRotation < 0) localRotation += 360;
        ApplyLocalRotation();
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

    void UpdatePosition(float dt) {
        majorAngle += dt * majorAngleSpeed;
        minorAngle += dt * minorAngleSpeed;
    }

}
