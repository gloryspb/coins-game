using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    public SceneLoadManager sceneLoadManager;

    public void Exit()
    {
        Application.Quit();
        Debug.Log("went to desktop");
    }

    public void Play()
    {
        sceneLoadManager = GetComponent<SceneLoadManager>();
        sceneLoadManager.LoadTargetScene("Game");
    }
}
