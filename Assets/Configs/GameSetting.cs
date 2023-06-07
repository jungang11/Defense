using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSetting
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        InitGameManager();
    }

    private static void InitGameManager()
    {
        if (GameManager.Instance == null)
        {
            GameObject gameManager = new GameObject();
            gameManager.name = "GameManager";
            gameManager.AddComponent<GameManager>();
        }
    }
}