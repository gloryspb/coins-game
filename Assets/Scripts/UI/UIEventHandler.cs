using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEventHandler : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject playerUI;
    public GameObject menuButtons;
    public GameObject exitDialogPanel;
    public GameObject settingsPanel;
    public GameObject deathScreen;
    public PlayerController playerController;
    public static UIEventHandler Instance;
    [SerializeField] private bool isDebugVersion;

    void Start()
    {
        Instance = this;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !Player.isDead)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                if (InventoryRenderer.inventoryIsOpen)
                {
                    InventoryRenderer.Inventory.CloseInventory();
                }
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
        settingsPanel.SetActive(false);
        deathScreen.SetActive(false);
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
        // sceneLoadManager = GetComponent<SceneLoadManager>();
        // sceneLoadManager.LoadTargetScene("MainMenu");
		SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void ExitToDesktop()
    {
        Application.Quit();
        if (isDebugVersion)
        {
            Debug.Log("went to desktop");
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void BackToPauseMenu()
    {
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
        PlayerControlTypeHolder.ControlType = (PlayerControlTypeHolder.ControlTypeEnum)Enum.Parse(typeof(PlayerControlTypeHolder.ControlTypeEnum), controlTypeStr);  
    }

    public void DeathScreen()
    {
        playerUI.SetActive(false);
        deathScreen.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void RestartScene()
    {
        // sceneLoadManager = GetComponent<SceneLoadManager>();
        Resume();
        Player.isDead = false;
        deathScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Game");
    }public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }public void LoadLevel3()
    {
        Resume();
        Time.timeScale = 1f;	
        SceneManager.LoadScene("Level3");
    }
}
