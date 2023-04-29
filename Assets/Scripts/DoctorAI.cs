using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoctorAI : MonoBehaviour
{

    private float speed = 10f;

    //-1 = Gauche, 0 = Bouge pas, 1 = Droite
    private int chosenDirectionX;
     //-1 = Haut, 0 = Bouge pas, 1 = Bas
    private int chosenDirectionY;
    private int timerDecision = 0;

    private Rigidbody2D rigidbody;
    Vector2 movementVector = Vector2.zero;

    public Rigidbody2D[] objectives;
    Rigidbody2D objectiveMinimum;

    //private Animator animator;
    //private SpriteRenderer SpriteRenderer;

    // Start is called before the first frame update
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        objectiveMinimum = objectives[0];
        foreach (Rigidbody2D objective in objectives) {
            
            if(Distance(objectiveMinimum.position) > Distance(objective.position)) {
                objectiveMinimum = objective;
            }
        }

        Decision();

    }

    // Update is called once per frame
    void Update()
    {

        Decision();

        float memoPosX = movementVector.x;
        float memoPosY = movementVector.y;

        movementVector.x = chosenDirectionX * Time.deltaTime * this.speed;
        movementVector.y = chosenDirectionY * Time.deltaTime * this.speed;

        movementVector.Normalize();

        rigidbody.velocity = movementVector * this.speed;
        
        
    }

    void Decision() {
            if(objectiveMinimum.position.x >= rigidbody.position.x) {
                chosenDirectionX = 1;
            }else if(objectiveMinimum.position.x  < rigidbody.position.x) {
                chosenDirectionX = -1;
            }

            if(objectiveMinimum.position.y  >= rigidbody.position.y) {
                chosenDirectionY = 1;
            }else if(objectiveMinimum.position.y  < rigidbody.position.y) {
                chosenDirectionY = -1;
            }
    }

    float Distance(Vector2 point) {
        return Mathf.Sqrt(Mathf.Pow(rigidbody.position.x-point.x,2) + Mathf.Pow(rigidbody.position.y-point.y,2));
    }
}
