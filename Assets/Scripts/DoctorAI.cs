using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    float testX = 3.5f;
    float testY = -2.69f;

    //public TilemapCollider2D walls;

    //private Animator animator;
    //private SpriteRenderer SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(timerDecision%1500 == 0) {
            Decision();
            /*
            chosenDirectionX = Random.Range(-1,2);
            chosenDirectionY = Random.Range(-1,2);
            while(chosenDirectionX == 0 && chosenDirectionY == 0) {
                chosenDirectionX = Random.Range(-1,2);
                chosenDirectionY = Random.Range(-1,2);
            }*/
        }
        
        float memoPosX = movementVector.x;
        float memoPosY = movementVector.y;

        movementVector.x = chosenDirectionX * Time.deltaTime * this.speed;
        movementVector.y = chosenDirectionY * Time.deltaTime * this.speed;

        

        movementVector.Normalize();

        if(memoPosX == movementVector.x || memoPosY == movementVector.y || (memoPosX == movementVector.x && memoPosY == movementVector.y)) {
            timerDecision = -1;
        }
        rigidbody.velocity = movementVector * this.speed;

        timerDecision++;
    }

    void Decision() {
        if(testX > rigidbody.position.x) {
            chosenDirectionX = 1;
        }else if(testX < rigidbody.position.x) {
            chosenDirectionX = -1;
        }

        if(testY > rigidbody.position.y) {
            chosenDirectionY = 1;
        }else if(testY < rigidbody.position.y) {
            chosenDirectionY = -1;
        }
    }
}
