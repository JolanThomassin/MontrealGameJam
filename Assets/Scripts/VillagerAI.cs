using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerAI : MonoBehaviour
{

    private float speed = 0.5f;

    //-1 = Gauche, 0 = Bouge pas, 1 = Droite
    private int chosenDirectionX;
     //-1 = Haut, 0 = Bouge pas, 1 = Bas
    private int chosenDirectionY;
    private int timerDecision = 0;
    private int randomFrequence;

    private Rigidbody2D rigidbody;
    Vector2 movementVector = Vector2.zero;

    public float PV;
    public bool plegued;
    private float deathByPlague = 0f;

    public Camera mainCamera;



    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        randomFrequence = Random.Range(0,500);
    }

    // Update is called once per frame
    void Update()
    {
        if(timerDecision%(500+randomFrequence) == 0) {
            chosenDirectionX = Random.Range(-1,2);
            chosenDirectionY = Random.Range(-1,2);
        }

        movementVector.x = chosenDirectionX * Time.deltaTime * this.speed;
        movementVector.y = chosenDirectionY * Time.deltaTime * this.speed;

        movementVector.Normalize();

        rigidbody.velocity = movementVector * this.speed;

        if(plegued) {

            gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
            deathByPlague += Time.deltaTime;
            mainCamera.orthographicSize+=Time.deltaTime/50;

            if(deathByPlague > 5f) {
                Destroy(gameObject);
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

        timerDecision++;
    }

    void OnCollisionEnter2D (Collision2D target) {

           this.speed += 0.1f;

    }
}
