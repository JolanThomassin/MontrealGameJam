using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorAI : MonoBehaviour
{

    private float speed = 50f;

    //-1 = Gauche, 0 = Bouge pas, 1 = Droite
    private int chosenDirectionX;
     //-1 = Haut, 0 = Bouge pas, 1 = Bas
    private int chosenDirectionY;
    private int timerDecision = 0;

    private Rigidbody2D rigidbody;
    Vector2 movementVector = Vector2.zero;

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
        if(timerDecision%30 == 0) {
            
            chosenDirectionX = Random.Range(-1,2);
            chosenDirectionY = Random.Range(-1,2);
            while(chosenDirectionX == 0 && chosenDirectionY == 0) {
                chosenDirectionX = Random.Range(-1,2);
                chosenDirectionY = Random.Range(-1,2);
            }
        }
        

        movementVector.x = chosenDirectionX * Time.deltaTime * this.speed;
        movementVector.y = chosenDirectionY * Time.deltaTime * this.speed;

        rigidbody.MovePosition(rigidbody.position + movementVector);

        timerDecision++;
    }
}
