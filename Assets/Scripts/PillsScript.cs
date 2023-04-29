using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillsScript : MonoBehaviour
{
    public GameManagerScript gameManager;

    public void Start() {
        if (!gameManager)
            gameManager = GameObject.Find("GameManager (GameObject)").GetComponent<GameManagerScript>();
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        gameManager.PickedPills();
        Destroy(this.gameObject);
    }
}
