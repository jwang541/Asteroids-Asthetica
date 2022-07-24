using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    ShipData sd;

    RawImage bar;
    Vector3 originalScale;


    void Start()
    {
        sd = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipData>();    
        bar = GetComponent<RawImage>();
        originalScale = bar.transform.localScale;
    }

    void Update()
    {
        bar.transform.localScale = new Vector3(originalScale.x, originalScale.y * Mathf.Max(0, sd.health), originalScale.z);
    }
}
