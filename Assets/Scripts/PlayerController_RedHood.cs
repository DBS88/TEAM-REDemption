using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_RedHood : MonoBehaviour {

    // Movement variables
    public float maxSpeed;

    // Jumping variables
    bool grounded = false;
    float groundCheckRadius = 1.0f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;

    Rigidbody2D RedHoodRB;
    Animator RedHoodAnim;
    bool facingRight;

    // Shooting
    public Transform gunTip;
    public GameObject bullet;
    float fireRate = 0.5f; // Time allowed that player can shoot every x amount of seconds.
    float nextFire = 0f; // Time allowed for player to shoot after the previous shot has been fired.

	// Use this for initialization
	void Start () {
        RedHoodRB = GetComponent<Rigidbody2D>();
        RedHoodAnim = GetComponent<Animator>();

        facingRight = true;
	}
	
	// Update is called once per frame
    void Update ()
    {
        if (grounded && Input.GetAxis("Jump") > 0)
        {
            grounded = false;
            RedHoodAnim.SetBool("isGrounded", grounded);
            RedHoodRB.AddForce(new Vector2(0, jumpHeight));
        }

        // Player shooting
        if (Input.GetAxisRaw("Fire1") > 0) fireBullet();
    }

	void FixedUpdate () {
        
        // start jump
        // Check if character is grounded, if no then character is falling
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); // Checks for ground
        RedHoodAnim.SetBool("isGrounded", grounded); // Player falling

        RedHoodAnim.SetFloat("verticalSpeed", RedHoodRB.velocity.y); // verticalSpeed is in the RHJumpBlendTree
        //end jump

        // Flipping sprites to face the direction of movement

        float move = Input.GetAxis("Horizontal");
        RedHoodAnim.SetFloat("movespeed", Mathf.Abs(move));

        RedHoodRB.velocity = new Vector2(move * maxSpeed, RedHoodRB.velocity.y); // when move button is pressed the player will move to maximum speed

        if(move>0 && !facingRight)
        {
            flip();
        }
        else if (move<0 && facingRight)
        {
            flip();
        }
		
	}

    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void fireBullet() // This code works in coherence with the ProjectileController script
    {
        if(Time.time > nextFire) // If current time is greater than next fire then, player can fire.
        {
            nextFire = Time.time + fireRate;
            
            // Flipping bullet direct
            if (facingRight)
            {
                Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            else if (!facingRight)
            {
                Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 180f))); // Flipped 180 degrees.
            }
        }
    }
}
