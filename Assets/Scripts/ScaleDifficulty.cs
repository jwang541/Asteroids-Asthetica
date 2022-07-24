using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDifficulty : MonoBehaviour
{
    GameManager gm;

    float initialSpawnSpeed = 0;
    float initialSplitSpeed = 0;
    float initialSpawnCapacity = 0;

    public float spawnSpeedScale = 0;
    public float splitSpeedScale = 0;
    public float spawnCapacityScale = 0;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();    

        initialSpawnSpeed = gm.spawnSpeed;
        initialSplitSpeed = gm.splitSpeed;
        initialSpawnCapacity = gm.spawnCapacity;
    }

    void Update()
    {
        gm.spawnSpeed = initialSpawnSpeed * (1 + spawnSpeedScale * gm.score);
        gm.splitSpeed = initialSplitSpeed * (1 + splitSpeedScale * gm.score);
        gm.spawnCapacity = initialSpawnCapacity * (1 + spawnCapacityScale * gm.score);
    }
}
