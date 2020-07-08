using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb2d; //This references the Rigidbody2D component of the enemy

    bool moveLeft;

    [SerializeField] float xMinBounds;
    [SerializeField] float xMaxBounds;

    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(moveLeft)
            rb2d.AddForce(new Vector2(-1, 0) * 100 * speed * Time.fixedDeltaTime);   
        
        else
            rb2d.AddForce(new Vector2(1, 0) * 100 * speed * Time.fixedDeltaTime);   
        
        if(moveLeft == false && transform.position.x >= xMaxBounds)
            moveLeft = true;
        else if(moveLeft == true && transform.position.x <= xMinBounds)
            moveLeft = false;
        
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 1f);
        Gizmos.DrawCube(new Vector3(xMinBounds, transform.position.y), new Vector3(0.35f, 0.35f, 1));
        Gizmos.color = new Color(0, 0, 1, 1f);
        Gizmos.DrawCube(new Vector3(xMaxBounds, transform.position.y), new Vector3(0.35f, 0.35f, 1));

    }

}
