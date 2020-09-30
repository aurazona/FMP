using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D PlayerRb; //used to initialise rigidboy
    private Animator PlayerAnimator; //used to intialise the animator
    public float GravityModifier; //used to change gravity
    public float MoveSpeed; //used to determine on-ground speed
    public float BhopBoost; //used to determine speed gained by bunnyhopping
    public int moveDirection; //used to determine the way the player is moving
    public int speed = 5; //sets the player speed
    public float jumpForce = 25.0f; //sets the force that the player is pushed up by whilst jumping
    public bool isOnGround; //checks for if the player is on the ground
    public int BhopTimer; //used to time the bunnyhop window
    public bool BhopEnabled; //used to start the bhopTimer
    public float MaxSpeed = 10.0f; //used to monitor player speed preventing 2FAST4U speeds.
    public float InvMaxSpeed = -10.0f; //used to monitor player speed preventing 2FAST4U speeds in reverse.
    public float InvSpeed; //used to store positive speed if player speed is negative
    public float PlayerSpeed; //used to store player speed for speed limit
    public int StopDrag = 1; //used to stop the player on a dime when on ground and not moving
    public static Transform PlayerLocationX; //used to store the player's X location
    public static Transform PlayerLocationY; //used to store the player's Y location
    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
        Physics.gravity *= GravityModifier;
        PlayerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerLocationX = transform.position.x;
        //PlayerLocationY = transform.position.y;
        CmdplayerMovement();
        SpeedLimit();
        if(BhopEnabled == true)
        {
            if (BhopTimer <= 60) //set to one second for testing, will probably be reduced on final build
            {
                BhopTimer++;
            }
            if (BhopTimer > 60)
            {
                BhopEnabled = false;
            }
        }
    }

    [Command]
    void CmdplayerMovement() //this function handles character movement and the direction they face, the direction they face will probably be changed to where they're aiming later down the line.
    {
        new Vector3();
        if (Input.GetKey(KeyCode.D)) //if D is pressed
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); //flips the character to face the right
            moveDirection = 1;
            Debug.Log("Moving to the right.");
            PlayerAnimator.SetBool("IsWalking", true);
            PlayerRb.AddForce(Vector3.right * speed, ForceMode2D.Impulse); //the part of the script that actually moves the character
        }
        if (Input.GetKey(KeyCode.A)) //if A is pressed
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); //flips the character to face the left
            moveDirection = -1;
            Debug.Log("Moving to the left.");
            PlayerAnimator.SetBool("IsWalking", true);
            PlayerRb.AddForce(Vector3.left * speed, ForceMode2D.Impulse); //the part of the script that actually moves the character
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            if (isOnGround == false)
            {
                return;
            }
            if (isOnGround == true)
            {
                PlayerRb.drag = StopDrag;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space)) //if we're on the ground and space is pressed
        {
            if (isOnGround == true)
            {
                if (BhopEnabled == false)
                {
                    PlayerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse); //add force to the up vector and multiply it by the jumpforce variable
                    isOnGround = false; //set isOnGround to false to disallow space programs
                }
                if (BhopEnabled == true)
                {
                    PlayerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse); //add force to the up vector and multiply it by the jumpforce variable
                    PlayerRb.AddForce(Vector3.right * BhopBoost * moveDirection, ForceMode2D.Impulse);
                    isOnGround = false; //set isOnGround to false to disallow space programs
                }
            }
        }
        else
        {
            PlayerAnimator.SetBool("IsWalking", false);
        }
    }

    void SpeedLimit()
    {
        if (PlayerRb.velocity.x > MaxSpeed)
        {
            PlayerRb.velocity = new Vector2(MaxSpeed, PlayerRb.velocity.y);
        }
        if (PlayerRb.velocity.x < InvMaxSpeed)
        {
            PlayerRb.velocity = new Vector2(InvMaxSpeed, PlayerRb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            BhopEnabled = true;
            BhopTimer = 0;
        }

    }
}
