using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject MainMenuScreen;
    public GameObject Levels;
    public GameObject GameSettings;
    public AudioSource audioSource;

    public static bool isAudio;
    public static AudioClip background;
    static AudioSource effectAudio;

    private void Start()
    {
        background = Resources.Load<AudioClip>("BACKGROUND");

        effectAudio = GetComponent<AudioSource>();

        isAudio = isFirstAudio();
    }

    public void MainMenu()                        // button-clicking functions
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void StartMenuScreen()
    {
        SceneManager.LoadScene("Menus");
    }

    public bool isFirstAudio()                              // start the game's background audio by default when you first start the game
    {
        
        if (SoundEffects.gameInitialized == false)          // if we have not started the game yet, automatically start the background music
        {
            return true;
        }
        

        else if (SoundEffects.backgroundAudio == false)     // otherwise, if we have already started the game and returned to the main menu, and if we have previously muted the background audio, remember the choice
        {
            return false;
        }

        else                                                // otherwise, we have already started the game but have not muted the background audio
        {
            return true;
        }
    }

    public void audioOn()
    {
        if (isAudio == false)
        {
            effectAudio.PlayOneShot(background);
            isAudio = true;
        }
    }

    public void audioOff()
    {
        if (isAudio == true)
        {
            effectAudio.Stop();
            isAudio = false;
        }
        
    }

    public void Quit()                             // works whether we are in the editor or not
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void GameSettingsScreen()
    {
        MainMenuScreen.SetActive(false);
        GameSettings.SetActive(true);
    }

    public void LevelScreen()
    {
        MainMenuScreen.SetActive(false);
        Levels.SetActive(true);
    }

    public void BackButton()
    {
        GameSettings.SetActive(false);
        Levels.SetActive(false);
        MainMenuScreen.SetActive(true);
    }
}