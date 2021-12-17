using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public LevelManager level;

    void Start()
    {
        // search for the type
        level = FindObjectOfType<LevelManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Access trigger here.
        if (other.name == "Player")
        {
            SoundEffects.Play("death");
            level.RespawnPlayer();
        }
    }
}
