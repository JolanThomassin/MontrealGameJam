using UnityEngine;
using System.Collections;


// An abstract class for bullets
// will be inherited by real bullet class
public abstract class Attack : MonoBehaviour
{
    public int damage;
    public int speed;
    public Animator animator;

    public Vector3 direction;  // the direction of this attack

    
    /// <summary>
    /// Function to set the shooting direction of this bullet
    /// </summary>
    /// <param name="direction"></param>
    public virtual void setDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    protected abstract void OnTriggerEnter2D(Collider2D collision);

}
