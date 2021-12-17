using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    bool onTop;
    GameObject bouncer;
    Animator anim;
    public Vector2 velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(onTop)
        {
            anim.SetBool("OnTrampoline", true);
            bouncer = other.gameObject;
        }
    }

    void OnTriggerEnter2D()
    {
        onTop = true;
    }

    void OnTriggerExit2D()
    {
        onTop = false;
        anim.SetBool("OnTrampoline", false);
    }

    void Jump()
    {
        bouncer.GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
