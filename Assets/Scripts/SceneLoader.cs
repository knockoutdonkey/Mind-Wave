using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader {

    public static void LoadNextLevel() {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextScene);
    }

    public static void RestartCurrentLevel() {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
