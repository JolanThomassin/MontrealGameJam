using System.Collections;
using UnityEngine;

public class DoctorHP : MonoBehaviour
{

    public int hp = 100;

    public bool isInfected = false;

    public bool isTrapped = false;

    public bool isDied = false;

    // Update is called once per frame
    void Update()
    {
            
    }

    public void GetInfected(int dmgOverTime = 1, float duration = 5, float tickRate = 1)
    {
        isInfected = true;
        StartCoroutine(DoTCoroutine(dmgOverTime, duration, tickRate));
    }

    public void LoseHp(int dmg)
    {
        hp -= dmg;
        if (hp < 0)
        {
            isDied = true;
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }

    IEnumerator DoTCoroutine(int dmgOverTime, float duration, float tickRate)
    {
        float timer = 0;
        while(timer < duration)
        {
            yield return new WaitForSeconds(tickRate);
            LoseHp(dmgOverTime);
            timer += tickRate;
        }
        isInfected=false;
    }

        
}
