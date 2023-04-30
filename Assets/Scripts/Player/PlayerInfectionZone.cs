using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfectionZone : MonoBehaviour
{
    private float radius = 2.5f;
    private Vector3 position;
    private GameObject doctor;
    [SerializeField] private LayerMask doctorMask;

    void Update()
    {
        Infect();
    }

    public void Infect()
    {
        position = transform.position;
        Collider2D raycastHit2D = Physics2D.OverlapCircle(position, radius, doctorMask);
        if (raycastHit2D != null)
        {
            var doctorHp = doctor.GetComponent<DoctorHP>();
            if (doctorHp && !doctorHp.isInfected)
            {
                doctorHp.GetInfected(infectionDmg, infectionDuration, infectionTickRate);
            }
        }
    }
}
