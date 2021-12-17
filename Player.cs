using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    [SerializeField]
    private float movementSpeed;
    float verticalMove = 0f;
    private bool crouch = false;
    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    public LayerMask whatIsGround;
    private bool isGroundedVar;
    private bool jump;
    [SerializeField]
    private float jumpForce;
    public float fallMultiplier = 12f;
    public float lowJumpMultiplier = 11.5f;
    public float riseMultiplier = 1f;
    public float gravity = -30f;
    private bool facingRight;
    public GameObject player;

    //*** Added function:
    public GameObject bulletL, bulletR;
    Vector2 bulletPositions;
    public float fireRate = 0.5f;
    float nextFire = 0.0f;
    private bool doubleJump;

    //*** WallSliding/WallJump
    public Transform wallCheck;
    private bool isTouchingWallLeft;
    private bool isTouchingWallRight;
    public float wallCheckDistance;
    private bool isWallSliding;
    public float wallSlideSpeed;

    //*** Added wall jump function
    public Vector2 onWallDirection;
    public Vector2 wallJumpDirection;
    public float onWallForce;
    public float wallJumpForce;
    private int facingDirection = 1;
    public float pushForce;

    void Start()
    {
        facingRight = true;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        onWallDirection.Normalize();  // 1
        wallJumpDirection.Normalize();
    }

    private void Update()
    {
        HandleInput();

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            SoundEffects.Play("bullet");
            nextFire = Time.time + fireRate;
            fire();
        }

        CheckIfWallSliding();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        isGroundedVar = IsGrounded();

        HandleMovement(horizontal);

        Flip(horizontal);

        HandleCrouch();

        HandleLayers();

        ResetValues();

        CheckSurroundings();

    }

    private void HandleMovement(float horizontal)
    {
        // if we are falling
        if (myRigidbody.velocity.y < 0)
        {
            myAnimator.SetBool("land", true);
            myRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (myRigidbody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            myRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (jump && !isWallSliding)
        {
            if (IsGrounded())
            {
                myRigidbody.AddForce(new Vector2(0, jumpForce));
                doubleJump = true;
            }
            else
            {

                if (doubleJump)
                {

                    doubleJump = false;
                    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0);
                    myRigidbody.AddForce(new Vector2(0, jumpForce));
                }
            }
            isGroundedVar = false;
            myAnimator.SetTrigger("jump");
        }
        else if (isWallSliding && horizontal == 0 && jump)
        {

            isWallSliding = false;
            Vector2 pushForce = new Vector2(onWallForce * onWallDirection.x * -facingDirection, onWallForce * onWallDirection.y);
            myRigidbody.AddForce(pushForce, ForceMode2D.Impulse);

        }
        else if ((isWallSliding || isTouchingWallLeft || isTouchingWallRight) && horizontal != 0 && jump)
        {

            isWallSliding = false;
            Vector2 pushForce = new Vector2(onWallForce * onWallDirection.x * 1, wallJumpForce * wallJumpDirection.y);
            myRigidbody.AddForce(pushForce, ForceMode2D.Impulse);

        }


        if (isGroundedVar)
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);    // x = -1, y = 0;
        }
        else
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed * 0.5f, myRigidbody.velocity.y);
        }

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        if (crouch)
        {
            myAnimator.SetBool("crouch", true);
            myAnimator.Play("Crouch");
        }
        else if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("crouch"))
        {
            myAnimator.SetBool("crouch", false);
        }

        if (isWallSliding)
        {

            if (myRigidbody.velocity.y < -wallSlideSpeed)
            {

                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, -wallSlideSpeed);

            }

        }
    }

    private void HandleInput()
    {
        verticalMove = Input.GetAxisRaw("Vertical");

        if (verticalMove == -1 && isGroundedVar)
        {
            myAnimator.Play("crouch");
            crouch = true;
        }
        else
        {
            crouch = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (jump)
            {
                crouch = false;
            }
            else
            {
                crouch = true;
                myAnimator.Play("Crouch");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || isWallSliding)
        {
            SoundEffects.Play("jump");
            jump = true;
        }
    }

    private void HandleCrouch()
    {
        if (crouch)
        {
            myRigidbody.velocity = Vector2.zero;
            myAnimator.Play("Crouch");
        }
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            if (!isWallSliding)
            {

                facingDirection *= -1;

                facingRight = !facingRight;

                Vector3 theScale = transform.localScale;

                theScale.x *= -1;

                transform.localScale = theScale;
            }
        }
    }

    private void ResetValues()
    {
        jump = false;
    }

    private bool IsGrounded()
    {
        if (myRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    // if the current collider isn't the player
                    if (colliders[i].gameObject != gameObject)
                    {
                        myAnimator.ResetTrigger("jump");
                        myAnimator.SetBool("land", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayers()
    {
        // used for rising and falling animations
        if (!isGroundedVar)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Moving Platform")
        {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "Moving Platform")
        {
            transform.parent = null;
        }
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
    }*/

    private void fire()
    {
        bulletPositions = transform.position;

        if (facingRight)
        {
            bulletPositions += new Vector2(+1f, 0.5f);
            Instantiate(bulletR, bulletPositions, Quaternion.identity);
        }
        else
        {
            bulletPositions += new Vector2(-1f, 0.5f);
            Instantiate(bulletL, bulletPositions, Quaternion.identity);
        }
    }

    private void CheckSurroundings()
    {

        var leftWall = transform.TransformDirection(Vector3.left);
        var rightWall = transform.TransformDirection(Vector3.right);

        isTouchingWallRight = Physics2D.Raycast(wallCheck.position, rightWall, wallCheckDistance, whatIsGround);
        isTouchingWallLeft = Physics2D.Raycast(wallCheck.position, leftWall, wallCheckDistance, whatIsGround);

    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));

    }

    private void CheckIfWallSliding()
    {

        if ((isTouchingWallLeft || isTouchingWallRight) && !isGroundedVar && myRigidbody.velocity.y < 0)
        {

            isWallSliding = true;
        }
        else
        {

            isWallSliding = false;
        }

    }

}




