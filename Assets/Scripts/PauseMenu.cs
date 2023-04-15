using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

//ok so currently the PauseMenu's buttons dont work
//this is only supposed to be used in single player
//so when we begin the multiplayer part of this game 
//we will have to create a different menu
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;
    public void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false; 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused)
            {
                ResumeGame();
            }
            else {
                PauseGame();
            }
        }
    }

    public void PauseGame() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        
    }

    public void GoToMainMenu() {
        throw new System.NotImplementedException();
    }

    public void OpenSettings() {
        throw new System.NotImplementedException();
    }

   
}
