using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    // --- Trap ----
    //Trap prefab
    [SerializeField]
    private GameObject trapPrefab;
    [SerializeField]
    private int maxNumberOfTraps = 3;
    [SerializeField]
    private int numberOfTraps;
 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            if (numberOfTraps < maxNumberOfTraps)
                PlaceTrap();
    }

    void PlaceTrap()
    {
        numberOfTraps += 1;
        Vector3 trapPosition = transform.position;
        GameObject trap = Instantiate(trapPrefab, trapPosition, Quaternion.identity);
    }
}
