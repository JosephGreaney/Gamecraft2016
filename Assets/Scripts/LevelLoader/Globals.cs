using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Globals
{
    private static int levelToLoad = 1;

    public static void LoadLevel(int level)
    {
        levelToLoad = level;
        TryLoadScene();
    }

    public static void RestartLevel()
    {
        TryLoadScene();
    }

    public static void LoadNextLevel()
    {
        levelToLoad++;
        TryLoadScene();
    }

    private static void TryLoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public static int GetLevelToLoad()
    {
        return levelToLoad;
    }

    public static void LoadLevelSelect()
    {
        SceneManager.LoadScene(0);
    }
}
