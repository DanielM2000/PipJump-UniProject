using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerController : MonoBehaviour {
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



    private int extraJumps;

    public int extraJumpsValue;

    void Start(){

        extraJumps = extraJumpsValue;

       //this GetComponent is used so that we can change the rigidbody via script.
        rb = GetComponent<Rigidbody2D>();
    }

    //Fixedupdate is used to manage all physics in the game.
    void FixedUpdate(){


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        //if statement used to check if player is moving right or left, then determines if its true or false
        if(facingRight == false && moveInput > 0)
        {
            flip();
        } else if (facingRight == true && moveInput < 0)
        {
            flip();
        }
    }


    void Update(){
        //used to reset extra jumps if player is grounded
        if(isGrounded == true){
            extraJumps = extraJumpsValue;
        }

        if(Input.GetKeyDown(KeyCode.Space) && extraJumps > 0){
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        } else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true){
            rb.velocity = Vector2.up * jumpForce;
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
