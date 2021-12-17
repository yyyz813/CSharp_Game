using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public GameObject currentCheckpoint;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RespawnPlayer()
    {

        // Testing
        Debug.Log("Respawn");
        player.transform.position = currentCheckpoint.transform.position;
    }

}
