using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    public float delay = 0;
    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay) Destroy(gameObject);
    }
}
