using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private float movementValue = 1f;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            rigidbody2d.AddForce(rigidbody2d.velocity + new Vector2(0, movementValue));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rigidbody2d.AddForce(rigidbody2d.velocity + new Vector2(movementValue,0));
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rigidbody2d.AddForce(rigidbody2d.velocity + new Vector2(-movementValue, 0));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rigidbody2d.AddForce(rigidbody2d.velocity + new Vector2(0, -movementValue));
        }
    }
}
