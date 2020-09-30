using UnityEngine;

public class player_script : MonoBehaviour
{
    public GameObject bullet;
    public GameObject Self;
    int moveDirection = 1;
    int speed = 5; //gives the player a base speed.
    public bool facingRight = false; //used to determine which way to fire the bullet
    public bool facingLeft = true; //used to determine which way to fire the bullet
    // Start is called before the first frame update
    public bool isOnGround = true;
    public float jumpForce;
    private Rigidbody2D playerRb;
    public AudioClip gunshot;
    private AudioSource playerAudio;
    public AudioClip deathsound;
    public int playerPitch = 5;
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<AudioSource>();
        playerAudio.pitch = playerPitch;
    }

    // Update is called once per frame
    void Update()
    {
        shootBullets(); //calls the shootBullets function
        playerMovement(); //calls the playerMovement function
    }
    void shootBullets() //this function handles the creation of bullets
    {
        //if Ctrl or mouse button was pressed, launch a bullet
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire");
            // Instantiate the bullet at the position and rotation of the player
            GameObject clone;
            clone = Instantiate(bullet, transform.position, transform.rotation);

            // get the rigidbody component
            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();

            // set the velocity
            rb.velocity = new Vector3(15 * moveDirection, 0, 0);

            // set the position close to the player
            rb.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1);

            //play audio on bullet fired
            playerAudio.PlayOneShot(gunshot);

            // Sets bullet rotation according to the facingLeft and facingRight booleans
            if (facingLeft == true)
            {
                rb.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            if (facingRight == true)
            {
                rb.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

        }
    }

    void playerMovement() //this function handles character movement and the direction they face
    {
        new Vector3();
        if (Input.GetKey(KeyCode.D)) //if D is pressed
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); //flips the character to face the right
            facingLeft = false; //disables facingLeft
            facingRight = true; //enabled facingRight
            moveDirection = 1;
            Debug.Log("Moving to the right.");
            transform.Translate(Vector3.left * Time.deltaTime * speed); //the part of the script that actually moves the character
        }
        if (Input.GetKey(KeyCode.A)) //if A is pressed
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); //flips the character to face the left
            facingRight = false; //disables facingRight
            facingLeft = true; //enables facingLeft
            moveDirection = -1;
            Debug.Log("Moving to the left.");
            transform.Translate(Vector3.left * Time.deltaTime * speed); //the part of the script that actually moves the character
        }
        if (Input.GetKeyDown(KeyCode.Space)) //if we're on the ground and space is pressed
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse); //add force to the up vector and multiply it by the jumpforce variable
            isOnGround = false; //set isOnGround to false to disallow space programs
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))

        {
            playerAudio.PlayOneShot(deathsound);
            Destroy(Self);
            Debug.Log("Should be dead.");
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        if (collision.gameObject.CompareTag("evilBullet"))
        {
            Destroy(Self);
        }

    }
}


