using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseManager;

    bool isPaused = false;

    void Start()
    {
        // Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isPaused = switchPause();
        }

    }

    bool switchPause()
    {
        if (Time.timeScale == 1)                 // game is currently not paused
        {
            Time.timeScale = 0;                 // pause
            PauseManager.SetActive(true);
            return (false);
        }
        else                                    // game is currently paused
        {
            PauseManager.SetActive(false);
            Time.timeScale = 1;                 // unpause
            return (true);
        }
    }
}