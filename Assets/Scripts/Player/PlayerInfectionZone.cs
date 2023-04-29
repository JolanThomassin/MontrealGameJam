using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfectionZone : MonoBehaviour
{
    public float radius = 2.5f;
    private Vector3 position;
    public GameObject doctor;
    [SerializeField] private LayerMask doctorMask;

    public int infectionDmg = 1;
    public int infectionDuration = 5;
    public int infectionTickRate = 1;

    void Update()
    {
        Infect();       
    }

    public void Infect()
    {
        position = transform.position;
        Collider2D raycastHit2D = Physics2D.OverlapCircle(position, radius, doctorMask);
        if(raycastHit2D != null)  
        {
            //TODO - Infect the doctor every tick
            Debug.Log("Doctor infected");

            var doctorHp = doctor.GetComponent<DoctorHP>();
            if (doctorHp && !doctorHp.isInfected)
            {
                doctorHp.GetInfected(infectionDmg, infectionDuration, infectionTickRate);
            }
        }
    }
}
