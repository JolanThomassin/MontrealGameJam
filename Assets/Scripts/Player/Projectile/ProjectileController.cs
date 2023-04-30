using System.Collections;
using UnityEngine;

namespace Assets.Prefabs.Projectile
{
    public class ProjectileController : MonoBehaviour
    {
        public Player_Bullet bullet;
        public Transform player;
        public float cooldown = 1f;
        public float timer = 0.5f;

        public bool shootFacing = false;
        public bool shootMouse = true;

        Vector3 rotation = new Vector3(0, 0, 0);

        [SerializeField]
        private float bulletSpeed = 2f;
        // Use this for initialization
        [SerializeField]
        private GameObject skill;
        [SerializeField]
        private GameObject anchor;
        private Vector2 skillGoBack = new Vector2(0, 65f);
        // Update is called once per frame
        void Update()
        {
            if(timer<0.1)
            {
                skill.transform.position = (Vector2)skill.transform.position+skillGoBack;
            }
            timer += Time.deltaTime;
            if (timer > 1)
            {
                timer = 1 + 1f;
            }

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            if (vertical > 0)
            {
                bullet.setDirection(new Vector3(0, bulletSpeed, 0));
                rotation = new Vector3(0, 0, 90);

            }
            if (vertical < 0)
            {
                bullet.setDirection(new Vector3(0, -bulletSpeed, 0));
                rotation = new Vector3(0, 0, -90);
            }
            if (horizontal > 0)
            {
                bullet.setDirection(new Vector3(bulletSpeed, 0, 0));
                rotation = new Vector3(0, 0, 0);
            }
            if (horizontal < 0)
            {
                bullet.setDirection(new Vector3(-bulletSpeed, 0, 0));
                rotation = new Vector3(0, 0, 180);
            }

            if (Input.GetKeyDown(KeyCode.Space) && timer > cooldown)
            {
                var shootDirection = Input.mousePosition;
                shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
                shootDirection = (shootDirection - transform.position);

                shootDirection.Normalize();
                timer = 0f;
                var bullet1 = Instantiate(bullet, player.transform.position, player.transform.rotation);
                // bullet1.transform.rotation = Quaternion.Euler(rotation);
                // bullet1.setDirection(bullet.direction);
                //var dir = player.transform.worldToLocalMatrix * Matrix4x4.Rotate(Quaternion.LookRotation(Vector3.forward, movement))* player.transform.up;
                var dir = player.transform.worldToLocalMatrix * Matrix4x4.Rotate(player.transform.rotation) * player.transform.up;
                if (shootFacing)
                {
                    bullet1.transform.rotation = Quaternion.Euler(rotation);
                    bullet1.setDirection(dir);
                }
                else if (shootMouse)
                {
                    skill.transform.position = anchor.transform.position;
                    bullet1.setDirection(shootDirection);
                    bullet1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    bullet1.transform.rotation *= Quaternion.LookRotation(Vector3.forward, shootDirection);
                }

            }
        }
    }
}