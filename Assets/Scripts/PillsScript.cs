using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillsScript : MonoBehaviour
{
    private GameManagerScript gameManager;

    public void Start() {
        gameManager = GameObject.Find("GameManager (GameObject)").GetComponent<GameManagerScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameManager.PickedPills();
        Destroy(this.gameObject);
    }
}
