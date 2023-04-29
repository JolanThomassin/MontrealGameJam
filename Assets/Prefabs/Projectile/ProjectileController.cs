using System.Collections;
using UnityEngine;

namespace Assets.Prefabs.Projectile
{
    public class ProjectileController : MonoBehaviour
    {

        public Player_Bullet1 bullet;
        public Transform player;
        public float cooldown = 1f;
        public float timer = 0.5f;

        Vector3 rotation = new Vector3(0,0,0);

        // Use this for initialization

        // Update is called once per frame
        void Update()
        {
            timer+=Time.deltaTime;

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            if (vertical > 0)
            {
                bullet.setDirection(new Vector3(0, 1, 0));
                rotation = new Vector3(0, 0, 90);
              
            }
            if (vertical < 0)
            {
                bullet.setDirection(new Vector3(0, -1, 0));
                rotation = new Vector3(0, 0, -90);
            }
            if (horizontal > 0)
            {
                bullet.setDirection(new Vector3(1, 0, 0));
                rotation = new Vector3(0, 0, 0);
            }
            if (horizontal < 0)
            {
                bullet.setDirection(new Vector3(-1, 0, 0));
                rotation = new Vector3(0, 0, 180);
            }

            if (Input.GetKeyDown(KeyCode.Space) && timer > cooldown)
            {
                timer = 0f;
                var bullet1 = Instantiate(bullet, player.transform.position, player.transform.rotation);
                bullet1.transform.rotation = Quaternion.Euler(rotation);
                bullet1.setDirection(bullet.direction);
            }
        }
    }
}