using System.Collections;
using UnityEngine;

public class TrapPlaced : MonoBehaviour
{
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Doctor")
        {
            collision.gameObject.GetComponent<DoctorHP>().GetTrapped(gameObject);
        }
    }

}
