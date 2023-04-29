using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public int numberOfPills;

    public void Start() {
        numberOfPills = 0;
    }

    public void PickedPills() {
        numberOfPills += 1;
        CheckVictory();
    }

    public void CheckVictory() {
        if (numberOfPills >= 5) {
            Debug.Log("The Doctor win the game");
        } 
    }
}
