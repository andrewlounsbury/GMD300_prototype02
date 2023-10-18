using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(); 
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
        Time.timeScale = 0;
        PausePanel.SetActive(true); 
    }

    public void Resume()
    {
        Time.timeScale = 1.0f; 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PausePanel.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Successfully");
        Application.Quit();
    }

}
