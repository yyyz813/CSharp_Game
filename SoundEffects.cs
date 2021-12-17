using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public static AudioClip soundFire, soundDeath, soundJump, music;
    static AudioSource audioEffect;

    public static bool gameInitialized = false;
    public static bool backgroundAudio = false;

    // Start is called before the first frame update
    void Start()
    {
        soundFire = Resources.Load<AudioClip>("BULLET");
        soundDeath = Resources.Load<AudioClip>("DEATH");
        soundJump = Resources.Load<AudioClip>("JUMP");
        music = Resources.Load<AudioClip>("BACKGROUND");

        audioEffect = GetComponent<AudioSource>();


        backgroundAudio = ButtonManager.isAudio;
        if (backgroundAudio == false)                           // if the player mutes the audio on the menu scene, the gameplay background audio will be muted as well
        {         
                audioEffect.Stop();
        }

        gameInitialized = true;
    }

    public static void Play(string sound)
    {

        switch (sound)
        {
            case "bullet":
                audioEffect.PlayOneShot(soundFire);
                break;
            case "death":
                audioEffect.PlayOneShot(soundDeath);
                break;
            case "jump":
                audioEffect.PlayOneShot(soundJump);
                break;

        }
    }

}