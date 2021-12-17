using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float directionX = 5f;
    float directionY = 0f;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(directionX, directionY);  // Assigned bullet velocity
        Destroy(gameObject, 3f);  // destroy game object in 3 seconds.
    }
}
