using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Stock the body
    private Rigidbody2D rigidbody2d;
    //Movement speed of the player
    [SerializeField]
    private float movementValue = 5f;

    // --- Dash ----
    //Dash speed of the player
    [SerializeField]
    private float dashSpeed = 30f;
    //Duration of the dash
    [SerializeField]
    private float dashDuration = 0.1f;
    //Cooldown of the dash
    [SerializeField]
    private float dashCooldown = 1f;
    //Timer for dash duration
    private float dashTimer = 0f;
    //Timer for dash cooldown
    private float dashCooldownTimer = 0f;
    //Flag for dashing
    private bool isDashing = false;
    // Flag to indicate if the player has already dashed
    private bool hasDashed = false;

    private Quaternion originalRotation;

    public Camera mainCamera;
 public Animator animator;

    public void PlagueStart()
    {
        //Store the rigid component at the start of the game
        rigidbody2d = GetComponent<Rigidbody2D>();

        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Move function
        Move();

        transform.rotation = originalRotation;

    }

    /**
     * Function that make the character from input
     */
    public void Move()
    {
        //Get the horizontal and vertical input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Create a new vector of those input
        Vector2 movement = new Vector2(horizontal, vertical);
        if (horizontal < 0)
        {
            animator.SetBool("WalkLeft", true);
            animator.SetBool("WalkRight", false);
        }
        else if (horizontal > 0)
        {
            animator.SetBool("WalkLeft", false);
            animator.SetBool("WalkRight", true);
        }
        else
        {
            animator.SetBool("WalkLeft", false);
            animator.SetBool("WalkRight", false);
        }
        if (vertical < 0)
        {
            animator.SetBool("WalkDown", true);
            animator.SetBool("WalkUp", false);
        }
        else if (vertical > 0)
        {
            animator.SetBool("WalkDown", false);
            animator.SetBool("WalkUp", true);
        }
        else
        {
            animator.SetBool("WalkDown", false);
            animator.SetBool("WalkUp", false);
        }
        //Check if dashing
        if (isDashing)
        {
            rigidbody2d.velocity = movement * dashSpeed;
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0)
            {
                isDashing = false;
                hasDashed = true; // Set the flag to true when the player has dashed
                dashCooldownTimer = dashCooldown;
            }
        }
        else
        {
            //Affect the movement to the velocity of the rigidbody to make it move
            rigidbody2d.velocity = movement * movementValue;

            // Stop the player when no input is detected
            if (movement.magnitude < 0.1f)
            {
                rigidbody2d.velocity = Vector2.zero;
            }
        }

        //Rotate the player based on the movement direction
        if (movement != Vector2.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, movement);
            movement = movement.normalized; // Store the dash direction
        }

        //Check if dash can be used
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0 && !hasDashed)
        {
            isDashing = true;
            dashTimer = dashDuration;
        }

        //Update dash cooldown timer
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
        else
        {
            hasDashed = false; // Reset the flag when the cooldown is over
        }

        //Camera follow player
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);

    }

}