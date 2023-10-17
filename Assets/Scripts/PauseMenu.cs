using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject PausePanel; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
       Time.timeScale = 0;
       PausePanel.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        PausePanel.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Debug.Log("successfully quit");
        Application.Quit();
    }
}
