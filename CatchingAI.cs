using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchingAI : MonoBehaviour
{

    public Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 6f;



    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Finding player's position and rotate towards player

        Vector3 position = player.position - transform.position;
        float angle = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg;  //converting to degree
        rb.rotation = angle;

        position.Normalize(); //1
        movement = position;

    }

    private void FixedUpdate()
    {
        moveEnemy(movement);
    }

    private void moveEnemy(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }
}