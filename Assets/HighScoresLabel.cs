using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class HighScoresLabel : MonoBehaviour
{
    Text label;

    void Start()
    {
        label = GetComponent<Text>();
        label.text = "";

        List<int> scores = new List<int>();

        if (PlayerPrefs.HasKey("scores")) {
            string json = PlayerPrefs.GetString("scores");

            if (JsonHelper.FromJson<int>(json) != null)
                scores = JsonHelper.FromJson<int>(json).ToList();
        }

        for (int i = 0; i < Mathf.Min(scores.Count, 5); i++) {
            label.text += "" + (i + 1) + ". " + scores[i] + "\n";
        }

        for (int i = scores.Count; i < 5; i++) {
            label.text += "" + (i + 1) + ". \n";
        }

    }
}
