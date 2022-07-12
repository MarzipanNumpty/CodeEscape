using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject PauseUI;
    [SerializeField] GameObject GameOverUI;
    private float previousTimeScale = 1f;
    private bool paused = false;
    public bool inTerminal;

    //Start is called before the first frame update
    void Start()
    {
        PauseUI.SetActive(false);
    }

    void Update()
    {
        //Check if the Game Over Menu is active and if no then continue with the check for input  
        if (GameOverUI.active == false && inTerminal == false)
        {
            //Check if the "Escape" key was pressed and if yes it brings up the pause menu UI.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log(inTerminal);
                Debug.Log("Works");
                paused = !paused;
                if (paused)
                {
                    PauseUI.SetActive(true);
                    previousTimeScale = Time.timeScale;
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                if (!paused)
                {
                    Resume();
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
        //If Game Over Menu is active on screen then don't show the Pause Menu
        else
        {
            PauseUI.SetActive(false);
        }


    }

    //Function for the resume button - Resumes time within the game and allows the player to continue playing.
    public void Resume()
    {
        paused = false;
        Time.timeScale = previousTimeScale;
        PauseUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Function for the restart button - Returns the player to their spawnpoint and resets the level.
    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 1;
        GameOverUI.SetActive(false);
        Debug.Log(Time.timeScale);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    //Function for the main menu button - Returns the player to the main menu.
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    //Function for quit button - Makes the application shut down.
    public void Quit()
    {
        Application.Quit();
    }

    public void changeTerminalStatus(bool terminalStatus)
    {
        inTerminal = terminalStatus;
        paused = false;
    }
}
