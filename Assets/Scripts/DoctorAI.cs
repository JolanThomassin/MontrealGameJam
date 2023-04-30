using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoctorAI : MonoBehaviour
{

    private float speed = 6f;

    //-1 = Gauche, 0 = Bouge pas, 1 = Droite
    private int chosenDirectionX;
     //-1 = Haut, 0 = Bouge pas, 1 = Bas
    private int chosenDirectionY;
    private int timerDecision = 0;

    private Rigidbody2D rigidbody;
    private CircleCollider2D circleCollider; 
    Vector2 movementVector = Vector2.zero;

    public List<Rigidbody2D> objectives;
    Rigidbody2D objectiveMinimum;
    int indiceTab = 0;

    int tempsDeBlocage = 0;
    int nbrAttenteBlocage = 0;

    int nbrPillCollected = 0;

    //private Animator animator;
    //private SpriteRenderer SpriteRenderer;

    // Start is called before the first frame update
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

        objectiveMinimum = objectives[0];
        int rolls = 0;
        foreach (Rigidbody2D objective in objectives) {
            
            if(Distance(objectiveMinimum.position) > Distance(objective.position)) {
                objectiveMinimum = objective;
                indiceTab = rolls;
            }
            rolls++;
        }

        Decision();

    }

    // Update is called once per frame
    void Update()
    {

        Decision();

        float memoPosX = movementVector.x;
        float memoPosY = movementVector.y;

        movementVector.x = chosenDirectionX  * this.speed;
        movementVector.y = chosenDirectionY  * this.speed;

        movementVector.Normalize();

        if(memoPosX == movementVector.x) {
            nbrAttenteBlocage++;
        }else if(memoPosY == movementVector.y) {
            nbrAttenteBlocage++;
        }else if(nbrAttenteBlocage != 0) {
            nbrAttenteBlocage--;
        }

        if(nbrAttenteBlocage > 300) {
            tempsDeBlocage++;
            transform.localScale = new Vector3(1.2f,1.2f,1.2f);
        }

        if(tempsDeBlocage > 0 && tempsDeBlocage < 30) {
            circleCollider.enabled = false;
        }else if (tempsDeBlocage >= 30 || tempsDeBlocage == 0) {
            tempsDeBlocage = 0;
            transform.localScale = new Vector3(1,1,1);
            circleCollider.enabled = true;
        }

        rigidbody.velocity = movementVector * this.speed;
        
        
    }

    void Decision() {
            if(nbrPillCollected < 4) {
                Vector2 v1 = objectiveMinimum.position;
                Vector2 v2 = rigidbody.position;

                if(v1.x > v2.x) {
                    chosenDirectionX = 1;
                }else if(v1.x  < v2.x -0.5f) {
                    chosenDirectionX = -1;
                }else {
                    chosenDirectionX = 0;
                }

                if(v1.y  > v2.y) {
                    chosenDirectionY = 1;
                }else if(v1.y  < v2.y -0.5f) {
                    chosenDirectionY = -1;
                }else {
                    chosenDirectionY = 0;
                }
            }
    }

    float Distance(Vector2 point) {
        return Mathf.Sqrt(Mathf.Pow(rigidbody.position.x-point.x,2) + Mathf.Pow(rigidbody.position.y-point.y,2));
    }

    void OnCollisionEnter2D(Collision2D infoCollision) {

        if (infoCollision.gameObject.name == "Circle") {

            objectives.RemoveAt(indiceTab);
            Destroy(infoCollision.gameObject);
            nbrPillCollected++;
            

            if(nbrPillCollected < 4) {
                objectiveMinimum = objectives[0];
                indiceTab = 0;
                int rolls = 0;
                foreach (Rigidbody2D objective in objectives) {
                    
                    if(Distance(objectiveMinimum.position) > Distance(objective.position)) {
                        objectiveMinimum = objective;
                        indiceTab = rolls;
                    }
                    rolls++;
                }
            }
            
        }
    }

    
}
