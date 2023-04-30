using System.Collections;
using UnityEngine;

public class DoctorMove : MonoBehaviour
{
    Animator animator;
    public Rigidbody2D rigidbody;
    public float speed = 0.5f;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("WalkRight", true);
        if (!rigidbody)
            rigidbody = GetComponent<Rigidbody2D>();
    }

    public void MoveLeft()
    {
        //transform.position += Vector3.left;
        animator.SetBool("WalkLeft", true);
        animator.SetBool("WalkRight", false);
    }
    public void MoveRight()
    {
        //transform.position += Vector3.right;
        animator.SetBool("WalkRight", true);
        animator.SetBool("WalkLeft", false);
    }

    public void MoveUp()
    {
        //transform.position += Vector3.up;
        animator.SetBool("WalkDown", false);
        animator.SetBool("WalkUp", true);
    }
    public void MoveDown()
    {
        //transform.position += Vector3.down;
        animator.SetBool("WalkDown", true);
        animator.SetBool("WalkUp", false);
    }


    private void Update()
    {

        if (rigidbody.velocity.x > 0)
            MoveRight();

        if (rigidbody.velocity.x < 0)
            MoveLeft();
        bool down = Input.GetKey(KeyCode.Keypad2);
        bool up = Input.GetKey(KeyCode.Keypad5);
        bool left = Input.GetKey(KeyCode.Keypad1);
        bool right = Input.GetKey(KeyCode.Keypad3);
        float horizontal = left ? -1 : right ? 1 : 0;
        float vertical = down ? -1 : up ? 1 : 0;
        if (left)
        {
            MoveLeft();
        }
        if (right)
        {
            MoveRight();
        }
        if (up)
        {
            MoveUp();
        }
        if (down)
        {
            MoveDown();
        }
        Vector2 vec = new Vector2(horizontal, vertical);
        rigidbody.velocity = vec * speed;
    }



}