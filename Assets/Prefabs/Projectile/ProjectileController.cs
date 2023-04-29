using System.Collections;
using UnityEngine;

namespace Assets.Prefabs.Projectile
{
    public class ProjectileController : MonoBehaviour
    {

        public Player_Bullet1 bullet;
        public Transform player;

        Vector3 rotation = new Vector3(0,0,0);

        // Use this for initialization

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                bullet.setDirection(new Vector3(0, 1, 0));
                rotation = new Vector3(0, 0, 90);
              
            }
            if (Input.GetKey(KeyCode.S))
            {
                bullet.setDirection(new Vector3(0, -1, 0));
                rotation = new Vector3(0, 0, -90);
            }
            if (Input.GetKey(KeyCode.D))
            {
                bullet.setDirection(new Vector3(1, 0, 0));
                rotation = new Vector3(0, 0, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                bullet.setDirection(new Vector3(-1, 0, 0));
                rotation = new Vector3(0, 0, 180);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                var bullet1 = Instantiate(bullet, player.transform.position, player.transform.rotation);
                bullet1.transform.rotation = Quaternion.Euler(rotation);
                bullet1.setDirection(bullet.direction);
            }
        }
    }
}