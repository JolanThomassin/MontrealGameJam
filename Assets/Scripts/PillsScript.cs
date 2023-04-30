using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillsScript : MonoBehaviour
{
    private GameManagerScript gameManager;

    public void Start() {
        gameManager = GameObject.Find("Manager").GetComponent<GameManagerScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Doctor"))
        {
            gameManager.PickedPills();
            Destroy(this.gameObject);
        }
       
    }
}
