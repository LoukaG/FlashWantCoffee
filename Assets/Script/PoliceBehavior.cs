using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceBehavior : MonoBehaviour
{
    public enum Direction
    {
        UP,
        LEFT,
        DOWN,
        RIGHT
    }

    private Direction shootingDirection;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shootChance = 0.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (Random.Range(0.0f, 1.0f) > shootChance) {
            GameObject bullet = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
            bullet.GetComponent<BulletBehavior>().shooter = this.gameObject;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            switch (shootingDirection)
            {
                case Direction.UP:
                    rb.AddForce(Vector3.up * bulletSpeed);
                    shootingDirection = Direction.RIGHT;
                    break;
                case Direction.LEFT:
                    rb.AddForce(Vector3.left * bulletSpeed);
                    shootingDirection = Direction.UP;
                    break;
                case Direction.DOWN:
                    rb.AddForce(-Vector3.up * bulletSpeed);
                    shootingDirection = Direction.LEFT;
                    break;
                case Direction.RIGHT:
                    rb.AddForce(-Vector3.left * bulletSpeed);
                    shootingDirection = Direction.DOWN;
                    break;

            }
        }
        else
        {
            Debug.Log("move");
            int randomDir = (int)Random.Range(0, 3);
            for(int i = 0; i < 4; i++)
            {
                randomDir++;
                if (randomDir > 3)
                    randomDir = 0;

                Vector2 dir = new Vector2(0, 0);

                switch (randomDir)
                {
                    case 0:
                        dir = Vector2.up;
                        break;
                    case 1:
                        dir = -Vector2.left;
                        break;
                    case 2:
                        dir = -Vector2.up;
                        break;
                    case 3:
                        dir = Vector2.left;
                        break;
                }

                GetComponent<BoxCollider2D>().enabled = false;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);
                GetComponent<BoxCollider2D>().enabled = true;
                Debug.Log(hit.distance);
                if (hit.distance >= 1)
                {
                    this.transform.position = new Vector3(this.transform.position.x + dir.x, this.transform.position.y + dir.y, 0);
                    break;
                }
            }
        }
    }
}
