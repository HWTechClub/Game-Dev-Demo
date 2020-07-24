using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb2d; //This references the Rigidbody2D component of the enemy

    [SerializeField] GameObject projectilePrefab;

    private float blobCooldownTime = 2.0f;

    private float timer = 0;


    bool moveLeft;

    //These are the x-axis bounds for the enemy's movement
    [SerializeField] float xMinBounds;
    [SerializeField] float xMaxBounds;

    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, 2.0f);
        }
    }

    void FixedUpdate()
    {
        //The enemy will continuously move left until they reach their xBounds then change directions.
        if (rb2d.velocity.y == 0)
        {
            if (moveLeft)
                rb2d.AddForce(new Vector2(-1, 0) * 100 * speed * Time.fixedDeltaTime);
            else
                rb2d.AddForce(new Vector2(1, 0) * 100 * speed * Time.fixedDeltaTime);
        }
        
        if(moveLeft == false && transform.position.x >= xMaxBounds)
            moveLeft = true;
        else if(moveLeft == true && transform.position.x <= xMinBounds)
            moveLeft = false;
        
    }

    //This draws the bounds of the enemy's movement.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 1f);
        Gizmos.DrawCube(new Vector3(xMinBounds, transform.position.y), new Vector3(0.35f, 0.35f, 1));
        Gizmos.color = new Color(0, 0, 1, 1f);
        Gizmos.DrawCube(new Vector3(xMaxBounds, transform.position.y), new Vector3(0.35f, 0.35f, 1));

    }

    void OnTriggerStay2D(Collider2D other)
    {
       
        if(other.gameObject.tag.Equals("Player") && timer == 0)
        {
            Vector2 angle = other.gameObject.transform.position - transform.position;

            GameObject projectileObject = Instantiate(projectilePrefab, rb2d.position + angle.normalized, Quaternion.identity);
            projectileObject.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg);

            EnemyProjectile projectile = projectileObject.GetComponent<EnemyProjectile>();
            projectile.Launch(angle.normalized, 300);
            timer = blobCooldownTime;
        }  

    }

}
