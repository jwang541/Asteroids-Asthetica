using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score = 0;

    public GameObject asteroidPrefab;
    public GameObject asteroidExplosionPrefab;
    public AudioSource asteroidExplosionSound;
    public float spawnSpeed = 0;
    public float splitSpeed = 0;
    public float spawnCapacity = 0;

    public AudioSource shipShootSound;
    public GameObject shipExplosionPrefab;
    public AudioSource shipHitSound;
    public AudioSource shipAccelerateSound;


    public LevelLoader titleLoader;
    public float transitionTime = 3f;


    bool isSaving = false;


    void Start()
    {
        
    }

    void Update()
    {
        var ship = GameObject.FindGameObjectWithTag("Player");
        if (ship == null && !isSaving) {

            isSaving = true;
            StartCoroutine(LoadTitleScreen());
        }
    }

    IEnumerator LoadTitleScreen() {
        SaveScore();
        
        yield return new WaitForSeconds(transitionTime);

        titleLoader.LoadNextLevel();
    }

    void SaveScore() {
        List<int> scores = new List<int>();
        if (PlayerPrefs.HasKey("scores")) {
            string json = PlayerPrefs.GetString("scores");

            if (JsonHelper.FromJson<int>(json) != null)
                scores = JsonHelper.FromJson<int>(json).ToList();
        }
        scores.Add(score);
        scores.Sort();
        scores.Reverse();

        if (scores.Count > 10) scores = scores.Take(10).ToList();
        PlayerPrefs.SetString("scores", JsonHelper.ToJson<int>(scores.ToArray()));
    }

}
