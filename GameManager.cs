﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("Menus");
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }

}