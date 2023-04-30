using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlagueCircle : MonoBehaviour
{
    
    private bool inRange = false;
    public bool canHit = false;
    private float hitCooldownTimer = 0f;
    public bool hasHit = false;
    private float hitTimer = 0f;
    private float hitDuration = 0.1f;
    private float hitCooldown = 1f;

    public Image myImage;

    public void Awake() {
        // Trouver l'objet de l'image
        myImage = GameObject.Find("InfectionProgression").GetComponent<Image>();
    }

    public void Update() {

        if (inRange && !hasHit && hitCooldownTimer <= 0f) {

            // Apply the hit and start the hit timer
            ApplyHit();
            hasHit = true;
            hitTimer = hitDuration;
            
        }

        if (hasHit) {

            // Update the hit timer
            hitTimer -= Time.deltaTime;
            if (hitTimer <= 0f) {
                // Reset the hit flag and start the hit cooldown timer
                hasHit = false;
                hitCooldownTimer = hitCooldown;
            }

        } else if (hitCooldownTimer > 0f) {

            // Update the hit cooldown timer
            hitCooldownTimer -= Time.deltaTime;

        }

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Doctor") {
            inRange = true;
        }
        if (other.gameObject.tag == "Villager") {
            other.gameObject.GetComponent<VillagerAI>().plegued = true;
            if(other.gameObject.transform.position.x >= transform.position.x) {
                    other.gameObject.GetComponent<VillagerAI>().chosenDirectionX = 1;
                }else {
                    other.gameObject.GetComponent<VillagerAI>().chosenDirectionX = -1;
                }

                if(other.gameObject.transform.position.y >= transform.position.y) {
                    other.gameObject.GetComponent<VillagerAI>().chosenDirectionY = 1;
                }else {
                    other.gameObject.GetComponent<VillagerAI>().chosenDirectionY = -1;
                }
        }
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Villager") {
            other.gameObject.GetComponent<VillagerAI>().PV--;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Doctor") {
            inRange = false;
        }
        if (other.gameObject.tag == "Villager") {
            other.gameObject.GetComponent<VillagerAI>().plegued = false;
        }

        
        
    }

    private void ApplyHit() {
        // Récupérer le RectTransform de l'image
        RectTransform rectTransform = myImage.rectTransform;

        // Récupérer le décalage maximum actuel
        Vector2 offsetMax = rectTransform.offsetMax;

        // Augmenter la valeur de la coordonnée x du décalage maximum
        offsetMax.x -= 100f; // par exemple, ajouter 100 pixels

        // Appliquer le nouveau décalage maximum
        rectTransform.offsetMax = offsetMax;
    } 

}
