using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchSceneWithButton : MonoBehaviour
{
    Button button;

    public LevelLoader loader;

    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(SwitchScene);
    }

    void SwitchScene() {
        loader.LoadNextLevel();
    }
}
