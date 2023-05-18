using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventHandler : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject playerUI;
    public GameObject menuButtons;
    public GameObject exitDialogPanel;
    public GameObject settingsPanel;
    public SceneLoadManager sceneLoadManager;
    public PlayerController playerController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        menuButtons.SetActive(true);
        playerUI.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        playerUI.SetActive(true);
        exitDialogPanel.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Exit()
    {
        exitDialogPanel.SetActive(true);
        menuButtons.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        sceneLoadManager = GetComponent<SceneLoadManager>();
        sceneLoadManager.LoadTargetScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void ExitToDesktop()
    {
        Application.Quit();
        Debug.Log("went to desktop");
        // Time.timeScale = 1f;
    }

    public void BackToPauseMenu()
    {
        pauseMenuUI.SetActive(true);
        menuButtons.SetActive(true);
        exitDialogPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void ShowSettingsMenu()
    {
        menuButtons.SetActive(false);
        settingsPanel.SetActive(true);
    }


    public void SetControlMethod(string controlTypeStr)
    {
        // почему-то нельзя поставить метод, который принимает значение енума на событие onclick кнопки, поэтому такой костыль
        playerController.controlType = (PlayerController.ControlType)Enum.Parse(typeof(PlayerController.ControlType), controlTypeStr);

        // Enum.TryParse<PlayerController.ControlType>(controlTypeStr, out PlayerController.ControlType controlType);
        // playerController.controlType = controlType;
        
    }
}
