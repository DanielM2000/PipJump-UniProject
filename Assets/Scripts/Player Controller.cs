using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //speed is how fast the player is moving.
    public float speed;
    //jumpforce is how high the player can jump.
    public float jumpForce;
    //moveInput detects if left or right button is pressed.
    private float moveInput;

    //Rigidbody 2D is connected to Unity.
    private Rigidbody2D rb;

    //facingright is used to flip the player sprite to face left or right gepending on the direction of movement. Default is set to true.
    private bool facingRight = true;

    //isGrounded is used to check if the player is on the ground or in mid air.
    private bool isGrounded;

    public Transform groundCheck;

    public float checkRadius;

    public LayerMask whatIsGround;

    //used to reference the animator in Unity.
    private Animator anim;

    // defaults the score to zero
    private int score = 0;

    //Used to dectect the FallDetector
    private Vector3 RespawnPoint;
    public GameObject FallDetector;

    //varriable for how many extra jumps the players can perform.
    private int extraJumps;
    //variable used to see how many extra jumps that the player has remaining.
    public int extraJumpsValue;

    void Start()
    {

        extraJumps = extraJumpsValue;

        //Grab references for rigidbody, animator and respawnpoint form object.
        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

        RespawnPoint = transform.position;
    }

    //Fixedupdate is used to manage all physics in the game.
    void FixedUpdate()
    {

        //this line of code generates a circle at the players feet, which will be used to check if the player is jumping.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

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

        // FallDetectors position changes based on players x position
        FallDetector.transform.position = new Vector2(transform.position.x, FallDetector.transform.position.y);


    }

    // If the player collides with the fall detector they will respawn either in the previous position or at the last checkpoint.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "FallDetector")
        {
            transform.position = RespawnPoint;
        }
        else if(collision.tag == "Checkpoint")
        {
            RespawnPoint = transform.position;
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("Appear");
        }
        else if (collision.tag == "Cheese")
        {
            score += 1;
            Debug.Log(score);
            collision.gameObject.SetActive(false);
        }
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
