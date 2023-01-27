using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class PlayerController : MonoBehaviour
{
    //speed is how fast the player is moving.
    public float speed;
    //jumpforce is how high the player can jump.
    public float jumpForce;
    //moveInput detects if left or right button is pressed.
    private float moveInput;

    private float horizontal;

    //Rigidbody 2D is connected to Unity.
    private Rigidbody2D rb;

    //facingright is used to flip the player sprite to face left or right gepending on the direction of movement. Default is set to true.
    private bool facingRight = true;

    //isGrounded is used to check if the player is on the ground or in mid air.
    private bool isGrounded;

    public Transform groundCheck;

    public float checkRadius;

    public LayerMask whatIsGround;

    private bool isWall;

    //this us used to check if the player is touching a wall.
    public LayerMask whatIsWall;

    //used to reference the animator in Unity.
    private Animator anim;
    //varriable for how many extra jumps the players can perform.
    private int extraJumps;
    //variable used to see how many extra jumps that the player has remaining.
    public int extraJumpsValue;

    //used to check whether the player is wall sliding and how fast the player is wall sliding.
    public Transform wallCheck;
    public bool isWallTouch;
    public bool isSilding;
    public float wallSlidingSpeed;
    public float wallJumpDuration;
    public Vector2 wallJumpForce;
    public bool wallJumping;
    private BoxCollider2D boxCollider;




    void Start()
    {

        extraJumps = extraJumpsValue;

        //Grab references for rigidbody, animator and respawnpoint form object.
        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

        boxCollider = GetComponent<BoxCollider2D>();
    }

    //Fixedupdate is used to manage all physics in the game.
    void FixedUpdate()
    {

        //this line of code generates a circle at the players feet, which will be used to check if the player is jumping.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isWallTouch = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.3f,1.6f), 0, whatIsWall);

        if(isWallTouch && !isWall && moveInput != 0)
        {
            isSilding = true;
        }
        else
        {
            isSilding = false;
        }

        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);



        //if statement used to check if player is moving right or left, then determines if its true or false
        if (facingRight == false && moveInput > 0)
        {
            flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            flip();
        }
       if (isSilding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
       if (wallJumping)
        {
            rb.velocity = new Vector2(-moveInput * wallJumpForce.x, wallJumpForce.y);
        }
       else
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
    }


    void Update()
    {
        //used to reset extra jumps if player is grounded
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("TakeOff");

        }
        else if(isSilding)
        {
            wallJumping = true;
            Invoke("StopWallJump", wallJumpDuration);
        }
        if (isGrounded == true)
        {
            anim.SetBool("IsJumping", false);
        }

        else
        {
            anim.SetBool("IsJumping", true);
        }
        //used to return true if the space key & the extrajumps are greater than 0, if the space button is pressed then extra jumps decreases by 1.
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
        //Set Animator Parameters.
        anim.SetBool("IsRunning", moveInput != 0);

        }

    void StopWallJump()
    {
        wallJumping = false;
    }
   
   
    //this flip is used to swap the x value from positve to negative depending on the else if satement.
    void flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
   
}
