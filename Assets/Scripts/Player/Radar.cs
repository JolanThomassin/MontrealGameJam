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

    public float speed = 50f;

    bool cooldown = false;

    [SerializeField] private AudioClip _clip;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Doctor");
    }

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
        SoundManager.Instance.PlaySoundEffects(_clip);
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

            var p = Instantiate(probe, transform.position, Quaternion.Euler(0, 0, 0));
            var vel = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            vel.Normalize();
            p.GetComponent<Rigidbody2D>().velocity = speed * vel;
            list.Add(p);
        }
        if (target)
        {
            float timer = duration;
            float step = 0.2f;
            while (timer > 0)
            {
                timer -= step;
                yield return new WaitForSeconds(step);
                foreach (var p in list)
                {
                    var rb = p.GetComponent<Rigidbody2D>();
                    var velt = (target.transform.position - rb.transform.position);
                    var vel = new Vector2(velt.x, velt.y);
                    vel.Normalize();
                    var curVel = rb.velocity;
                    curVel.Normalize();
                    var midVel = (vel - curVel) / 2.0f + curVel;

                    midVel.Normalize();
                    midVel *= speed;
                    rb.velocity = midVel;
                    rb.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    rb.transform.rotation *= Quaternion.LookRotation(Vector3.forward, velt);
                }
            }
        }
        yield return new WaitForSeconds(duration-1);
        foreach (var p in list)
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