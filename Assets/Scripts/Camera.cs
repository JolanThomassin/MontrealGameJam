using UnityEngine;
public class Camera : MonoBehaviour
{
    public GameObject followTarget;

    private void Update()
    {
        float z = transform.position.z;
        transform.position = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, z);
    }

}
