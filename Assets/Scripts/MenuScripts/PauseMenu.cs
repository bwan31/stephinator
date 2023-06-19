using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject player;
    public GameObject pauseMenuUI;
    [Header("it hides your hud when you Pause")] // example | "E" to open door |
    public GameObject HUD;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        player.GetComponent<PlayerManager>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        HUD.SetActive(true);

    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        player.GetComponent<PlayerManager>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        HUD.SetActive(false);

    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        isPaused = false;
        player.GetComponent<PlayerManager>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    // public void QuitGame()
    // {
    //     Debug.Log("Quitting game...");
    //     Application.Quit();
    // }
}