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
    void Start()
    {
        //Store the rigid component at the start of the game
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move function
        Move();
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
        Vector2 vec= new Vector2(horizontal, vertical);
        //Normalize the vecto
        vec.Normalize();
        //Affect the movement to the velocity of the rigidbody to make it move
        rigidbody2d.velocity = vec * movementValue;
    }
}