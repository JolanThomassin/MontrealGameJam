using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DoctorHP : MonoBehaviour
{

    public int hp = 100;

    public bool isInfected = false;

    public bool isTrapped = false;

    public bool isDied = false;

    Rigidbody2D rb;

    public Text HPText;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        HPText.text = hp.ToString();
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
        HPText.text = hp.ToString();
    }

    IEnumerator DoTCoroutine(int dmgOverTime, float duration, float tickRate)
    {
        float timer = 0;
        while (timer < duration)
        {
            yield return new WaitForSeconds(tickRate);
            LoseHp(dmgOverTime);
            timer += tickRate;
        }
        isInfected = false;
    }

    public void GetTrapped(GameObject trap, float duration = 3)
    {
        isTrapped = true;
        StartCoroutine(TrapCoroutine(duration, trap));
    }

    IEnumerator TrapCoroutine(float duration, GameObject trap)
    {
        var curConstraints = rb.constraints;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(duration);
        isTrapped = false;
        rb.constraints = curConstraints;
        Destroy(trap);
    }


}