using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
        {
            SceneManager.LoadScene("Finish");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;                     // if the player exits the game in the "Pause" state, the game will begin anew in the "Play" state instead of the "Pause" state
            SceneManager.LoadScene("Menus");
        }

    }
}
