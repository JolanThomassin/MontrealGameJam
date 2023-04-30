using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerAI : MonoBehaviour
{

    private float speed = 0.5f;

    //-1 = Gauche, 0 = Bouge pas, 1 = Droite
    public int chosenDirectionX;
     //-1 = Haut, 0 = Bouge pas, 1 = Bas
    public int chosenDirectionY;
    private int timerDecision = 0;
    private int randomFrequence;

    private Rigidbody2D rigidbody;
    Vector2 movementVector = Vector2.zero;

    public float PV;
    public bool plegued;
    private float deathByPlague = 0f;
    public bool dead;
    public bool reanimation = false;
    public bool isTrapped = false;
    public Camera mainCamera;

    public float Speed { get => speed; set => speed = value; }
    private float stunCouldown = 0f;



    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        randomFrequence = Random.Range(0,500);
    }

    // Update is called once per frame
    void Update()
    {
        //Stun the villager for 3 sec if he is trapped
        if (speed == 0f)
        {
            if (stunCouldown > 2f)
            {
                speed = 0.5f;
            }
            else
            {
                stunCouldown += Time.time;
            }
        }

        if (timerDecision%(500+randomFrequence) == 0) {
            chosenDirectionX = Random.Range(-1,2);
            chosenDirectionY = Random.Range(-1,2);
        }

        movementVector.x = chosenDirectionX * Time.deltaTime * this.speed;
        movementVector.y = chosenDirectionY * Time.deltaTime * this.speed;

        movementVector.Normalize();

        rigidbody.velocity = movementVector * this.speed;

        if(!dead) {
            if(plegued) {

                gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
                deathByPlague += Time.deltaTime;
                mainCamera.orthographicSize+=Time.deltaTime/50;

                if(deathByPlague > 5f) {
                    dead = true;
                    speed = speed/10f;
                }
            }else {
                gameObject.GetComponent<Renderer>().material.color = new Color(0, 200, 0);
                if(deathByPlague > 0f) {
                    deathByPlague-= Time.deltaTime*2;
                    mainCamera.orthographicSize-=Time.deltaTime/50*2;
                }else if(PV < 500f) {
                    gameObject.GetComponent<Renderer>().material.color = new Color(100, 255, 100);
                    PV+=10;
                }
            }
        }else if(reanimation) {
            gameObject.GetComponent<Renderer>().material.color = new Color(0, 200, 0);
            if(deathByPlague > 0f) {
                deathByPlague-= Time.deltaTime*2;
                mainCamera.orthographicSize-=Time.deltaTime/50*2;
            } else {
                dead = false;
            }
            reanimation = false;
        }
        

        timerDecision++;
    }

    void OnCollisionEnter2D (Collision2D target) {
        if(target.gameObject.tag == "Villager") {
            this.speed += 0.1f;
            if(target.gameObject.GetComponent<VillagerAI>().plegued || target.gameObject.GetComponent<VillagerAI>().dead) {
                if(target.gameObject.transform.position.x >= transform.position.x) {
                    chosenDirectionX = -1;
                }else {
                    chosenDirectionX = 1;
                }

                if(target.gameObject.transform.position.y >= transform.position.y) {
                    chosenDirectionY = 1;
                }else {
                    chosenDirectionY = -1;
                }
            }
        }

        if(target.gameObject.tag == "PlayerBullet") {
            this.speed += 2f;
                if(target.gameObject.transform.position.x >= transform.position.x) {
                    chosenDirectionX = -1;
                }else {
                    chosenDirectionX = 1;
                }

                if(target.gameObject.transform.position.y >= transform.position.y) {
                    chosenDirectionY = 1;
                }else {
                    chosenDirectionY = -1;
                }
        }
    }

    void OnTriggerStay2D(Collider2D target)
        {
            if(target.gameObject.tag == "Doctor") {
                if(target.gameObject.transform.position.x >= transform.position.x) {
                    chosenDirectionX = 1;
                }else {
                    chosenDirectionX = -1;
                }

                if(target.gameObject.transform.position.y >= transform.position.y) {
                    chosenDirectionY = -1;
                }else {
                    chosenDirectionY = 1;
                }
        } 
        }
    public void GetTrapped(GameObject trap, float duration = 3)
    {
        isTrapped = true;
        StartCoroutine(TrapCoroutine(duration, trap));
    }
    IEnumerator TrapCoroutine(float duration, GameObject trap)
    {
        var curConstraints = rigidbody.constraints;
        rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(duration);
        isTrapped = false;
        rigidbody.constraints = curConstraints;
        Destroy(trap);
    }
}
