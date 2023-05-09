using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
        Debug.Log("went to desktop");
    }

    public void Play()
    {
        SceneLoadManager sceneLoadManager = new SceneLoadManager();
        sceneLoadManager.LoadTargetScene("Game");
    }
}
