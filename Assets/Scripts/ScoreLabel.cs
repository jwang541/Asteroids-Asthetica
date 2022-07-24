using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ScoreLabel : MonoBehaviour
{
    GameManager gm;
    Text text;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = "" + gm.score;
    }
}
