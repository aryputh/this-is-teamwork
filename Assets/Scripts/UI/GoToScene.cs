using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    [SerializeField] private bool manuallySetScene;

    public void MoveToScene(string sceneName)
    {
        if (manuallySetScene) {
            SceneManager.LoadScene(sceneName);
        } else {
            SceneManager.LoadScene(PlayerPrefs.GetInt("currentScene", 1));
        }
    }
}
