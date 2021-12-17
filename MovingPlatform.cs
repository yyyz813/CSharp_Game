using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject platform;
    public float moveSpeed = 2;

    public GameObject startPoint;
    public GameObject endPoint;

    public bool onTop = false;
    public bool goUp = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!goUp)
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, startPoint.transform.position, Time.deltaTime * moveSpeed);
        }
        else if (platform.transform.position == endPoint.transform.position)
        {
            goUp = false;
        }
        else if (goUp)
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, endPoint.transform.position, Time.deltaTime * moveSpeed);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            onTop = true;
            goUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        onTop = false;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        
    }
}
