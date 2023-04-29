using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet1 : Bullet
{
    public float maxDistance;  // max distance of this bullet can reach
    public bool moveOnStart = false;
    public bool test = false;

    Vector3 initialPos;

    // Start is called before the first frame update
    protected override void Start()
    {
        initialPos = transform.position;
        base.Start();  // call the start() in the base class
        if (moveOnStart)
            Move(this.transform.position, direction, maxDistance);  // start moving this bullet (coroutine)
    }

    public void Shoot()
    {
        Move(this.transform.position, direction, maxDistance);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (test)
        {
            test = false;
            transform.position = initialPos;
            Move(this.transform.position, direction, maxDistance);
        }
    }

    private void Update()
    {

    }

    /// <summary>
    /// This function will be called automatically when the bullet is colliding with any collider
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Monster" || collision.gameObject.tag == "Ground")
        //{
        //    if (collision.gameObject.tag == "Monster") {
        //       // if (collision.gameObject.GetComponent<Character>().isInvincible) {
        //            Debug.Log("monster dodged");
        //       //     return;
        //   // }
        //    }
        //    stop();  // stop the motion of bullet when hits a monster
        //}
        //if (collision.gameObject.tag != "Player")
        //    stop();
        if (collision.gameObject.name == "TilemapWall")
        {
            stop();
        }
    }




}
