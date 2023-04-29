using System.Collections;
using UnityEngine;

public class DoctorMove : MonoBehaviour
{
    Animator animator;

    public bool moveLeft = false;
    public bool moveRight = false;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("WalkRight", true);
    }

    public void MoveLeft()
    {
        animator.SetBool("WalkLeft", true);
        animator.SetBool("WalkRight", false);
    }
    public void MoveRight()
    {
        animator.SetBool("WalkRight", true);
        animator.SetBool("WalkLeft", false);
    }

    private void Update()
    {
        if (moveLeft)
        {
            MoveLeft();
            moveLeft = false;
        }
        if (moveRight)
        {
            MoveRight();
            moveRight = false;
        }

    }

}