using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoctorAI : MonoBehaviour
{

    private float speed = 1f;
    public int PV;

    //-1 = Gauche, 0 = Bouge pas, 1 = Droite
    private int chosenDirectionX;
    //-1 = Haut, 0 = Bouge pas, 1 = Bas
    private int chosenDirectionY;
    private int timerDecision = 0;

    private Rigidbody2D rigidbody;
    private CircleCollider2D circleCollider; 
    Vector2 movementVector = Vector2.zero;

    public LevelGenerator levelGenerator;

    public List<GameObject> objectives;
    GameObject objectiveMinimum;
    int indiceTab = 0;

    int tempsDeBlocage = 0;
    int nbrAttenteBlocage = 0;

    int nbrPillCollected = 0;

    bool switchMur = false;


    //private Animator animator;
    //private SpriteRenderer SpriteRenderer;

    // Start is called before the first frame update
    
    public void DoctorStart()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
       
        objectives = levelGenerator.listPills;

        objectiveMinimum = objectives[0];
        int rolls = 0;
        foreach (GameObject objective in objectives) {
            
            if(Distance(objectiveMinimum.transform.position) > Distance(objective.transform.position)) {
                objectiveMinimum = objective;
                indiceTab = rolls;
            }
            rolls++;
        }
    }



    // Update is called once per frame
    void Update()
    {

        if (objectives != null) {
            Decision();
        }

        movementVector.x = chosenDirectionX  * this.speed;
        movementVector.y = chosenDirectionY  * this.speed;
        

        if(switchMur) {
            circleCollider.enabled = false;
            transform.localScale = new Vector3(1.2f,1.2f,1.2f);
            tempsDeBlocage++;
        }
        if (tempsDeBlocage > 200) {
            circleCollider.enabled = true;
            transform.localScale = new Vector3(1,1,1);
            tempsDeBlocage = 0;
            switchMur = false;
        } 
    
        rigidbody.velocity = movementVector * this.speed;

        //WIN CONGRATS !!!!
        //Debug.Log(PV);
        if(PV <= 0) {
            Destroy(gameObject);
            nbrPillCollected = 10;
            levelGenerator.PrintDoctorDead();
        }
        
        
    }

    void Decision() {
            if(nbrPillCollected < 10) {
                Vector2 v1 = objectiveMinimum.transform.position;
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
            else
            {
                chosenDirectionY = 0;
            }
        }

    float Distance(Vector2 point)
    {
        return Mathf.Sqrt(Mathf.Pow(rigidbody.position.x - point.x, 2) + Mathf.Pow(rigidbody.position.y - point.y, 2));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("1F");
        if (other.gameObject.tag == "Wall") {
            Debug.Log("2F");
            switchMur = true;
        }
        if (other.gameObject.tag == "Pill") {

            objectives.RemoveAt(indiceTab);
            Destroy(other.gameObject);
            nbrPillCollected++;


            if(nbrPillCollected < 10) {
                objectiveMinimum = objectives[0];
                indiceTab = 0;
                int rolls = 0;
                foreach (GameObject objective in objectives)
                {

                    if (Distance(objectiveMinimum.transform.position) > Distance(objective.transform.position))
                    {
                        objectiveMinimum = objective;
                        indiceTab = rolls;
                    }
                    rolls++;
                }
            }else {
                //Destroy(gameObject);

                //lose loser
            }

        }
        if (other.gameObject.tag == "Plague") {
            PV--;
            Debug.Log(PV);
        }
    }
}
