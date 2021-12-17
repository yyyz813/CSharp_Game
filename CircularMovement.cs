using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    [SerializeField]
    Transform rotationCenter;

    [SerializeField]
    float rotationRadius = 7f, angularSpeed = 4f;

    float posX, posY, angle = 0f;

    public LevelManager level;

    void Start()
    {
        // search for the type
        level = FindObjectOfType<LevelManager>();

    }

    // Update is called once per frame
    void Update()
    {
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius;
        transform.position = new Vector2(posX, posY);
        angle = angle + Time.deltaTime * angularSpeed;

        if (angle >= 360f)
        {
            angle = 0f;
        }
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
