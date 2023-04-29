using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillsScript : MonoBehaviour
{
    private WinCondition winConditionGO;

    public void Start() {
        winConditionGO = GameObject.Find("WinCondition (GameObject)").GetComponent<WinCondition>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        winConditionGO.PickedPills();
        Destroy(this.gameObject);
    }
}
