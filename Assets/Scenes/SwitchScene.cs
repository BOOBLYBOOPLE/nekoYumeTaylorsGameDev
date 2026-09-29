using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void SwitchTo(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void SwitchTo(int sceneIndex)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneIndex);
    }

    public void SwitchToNextScene()
    {
        Time.timeScale = 1f;
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        if (currentIndex + 1 < totalScenes)
            SceneManager.LoadScene(currentIndex + 1);
        else
            Debug.Log("已经是最后一个场景。");
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}