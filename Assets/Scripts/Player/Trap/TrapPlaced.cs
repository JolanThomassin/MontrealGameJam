using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlaced : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Doctor"))
        {
            collision.gameObject.GetComponent<DoctorHP>().GetTrapped(gameObject);
            //Animation and destroy
        }
        else if(collision.gameObject.tag.Equals("Villager"))
        {
            collision.gameObject.GetComponent<VillagerAI>().GetTrapped(gameObject);
        }
    }

    
}
