using UnityEngine;
using System.Collections;


// An abstract class for bullets
// will be inherited by real bullet class
public abstract class Bullet : Attack
{
    private CapsuleCollider2D bulletCollider;      //The collider component attached to this object(the bullet game object which has a script that derives this abstract class).
    private Rigidbody2D rb;                //The Rigidbody2D component attached to this object(the bullet game object which has a script that derives this abstract class).

    private IEnumerator coroutine;  // store the coroutine in a field variable
    //private float speedFactor = 70f;


    // Use this for initialization
    //Protected, virtual functions can be overridden by inheriting classes.
    protected virtual void Start()
    {
        //Get a component reference to this object's BoxCollider2D
        bulletCollider = GetComponent<CapsuleCollider2D>();

        //Get a component reference to this object's Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Get a component reference to this object's Animator
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }


    protected void Move(Vector3 currentPosition, Vector3 direction, float maxRange)
    {
        Vector3 destination = currentPosition + direction * maxRange;

        // start SmoothMovement co-routine passing in the Vector2 end as destination
        this.coroutine = ConstantMovement(destination);
        StartCoroutine(coroutine);

    }

    //Co-routine for moving units from current position to the destination, takes a parameter destination to specify where to move to.
    protected IEnumerator ConstantMovement(Vector3 destination)
    {
        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = (transform.position - destination).sqrMagnitude;

        // calculate the moving direction
        Vector3 direction = (destination - (Vector3)rb.position).normalized;
        // set a constant velocity for the bullet
        rb.velocity = direction * speed;  //Time.deltaTime * speedFactor

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > 0.05f)
        {
            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - destination).sqrMagnitude;

            // Yielding of any type, including null, results in the execution coming back on a later frame (next frame)
            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }

        // when the co-routine for movement is done, destroy this bullet
        Destroy(this.gameObject);
    }

    //Co-routine for moving units from one space to next, takes a parameter destination to specify where to move to.
    protected IEnumerator SmoothMovement(Vector3 destination)
    {
        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = (transform.position - destination).sqrMagnitude;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > 0.05f)
        {
            
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPostion = Vector3.MoveTowards(rb.position, destination, speed * Time.deltaTime);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rb.MovePosition(newPostion);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - destination).sqrMagnitude;

            // Yielding of any type, including null, results in the execution coming back on a later frame (next frame)
            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }

        // when the co-routine for movement is done, destroy this bullet
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Funtion to stop the motion of bullet
    /// </summary>
    protected void stop()
    {
        StopCoroutine(this.coroutine);
        rb.velocity = Vector3.zero;

        animator.SetTrigger("IsHit"); // trigger the hitting animation
        // destroy this bullet with a delay to play the is hit animation
        Destroy(this.gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
        

    }

}
