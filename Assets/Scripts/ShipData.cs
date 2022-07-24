using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData : MonoBehaviour
{
    public float health = 0;
    public float healthRegenerationRate = 0;

    void Start()
    {
        
    }

    void Update()
    {
        float dt = Time.deltaTime;

        health = Mathf.Min(1.0f, health + dt * healthRegenerationRate);
    }
}
