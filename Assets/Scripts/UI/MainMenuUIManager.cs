using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    public SceneLoadManager sceneLoadManager;
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    [SerializeField] private bool isDebugVersion;

    public void Exit()
    {
        Application.Quit();
        if (isDebugVersion)
        {
            Debug.Log("went to desktop");
        }
    }

    public void Play()
    {
        sceneLoadManager = GetComponent<SceneLoadManager>();
        sceneLoadManager.LoadTargetScene("Game");
    }

    public void ShowSettingsPanel()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void SetControlMethod(string controlTypeStr)
    {
        PlayerControlTypeHolder.ControlType = (PlayerControlTypeHolder.ControlTypeEnum)Enum.Parse(typeof(PlayerControlTypeHolder.ControlTypeEnum), controlTypeStr);  
    }
}
