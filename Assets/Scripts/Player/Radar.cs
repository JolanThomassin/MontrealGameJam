using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public Material spotlightMat;

    public GameObject probe;

    public GameObject target;

    public int numSpawns = 12;

    public float duration = 5f;

    public float speed = 5f;

    bool cooldown = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R) && !cooldown)
        {
            EnableProbes();
        }
    }

    public void EnableProbes()
    {
        StartCoroutine(spawnProbes());
        
    }

    IEnumerator spawnProbes()
    {
        cooldown = true;
        spotlightMat.SetFloat("_VisibleDistance", 1.0f);
        List<GameObject> list = new List<GameObject>();
        for (float i = 0; i < numSpawns; ++i)
        {
            float angle = i / numSpawns * 2 * Mathf.PI;
            
            var p = Instantiate(probe, transform.position, Quaternion.Euler(0, 0, angle));
            var vel = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            vel.Normalize();
            p.GetComponent<Rigidbody2D>().velocity = speed * vel;
            list.Add(p);
        }
        if (target)
        {
            yield return new WaitForSeconds(0.5f);
            foreach (var p in list)
            {
                var vel = (target.transform.position - transform.position);
                vel.Normalize();
                p.GetComponent<Rigidbody2D>().velocity = speed * vel;
            }
        }
        yield return new WaitForSeconds(duration);
        foreach(var p in list)
        {
            Destroy(p);
        }
        spotlightMat.SetFloat("_VisibleDistance", 0.2f);
        cooldown = false;
    }

    public void DisableProbes()
    {
        spotlightMat.SetFloat("_VisibleDistance", 0.2f);
    }
}