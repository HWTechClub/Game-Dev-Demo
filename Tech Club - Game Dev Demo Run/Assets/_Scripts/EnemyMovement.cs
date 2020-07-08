using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    bool moveLeft;
    [SerializeField] Rigidbody2D rb2d; //This references the Rigidbody2D component of the enemy
    Vector2 position;


    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
        position = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(moveLeft)
            rb2d.AddForce(new Vector2(-1, 0) * 100 * speed * Time.fixedDeltaTime);   
        
        else
            rb2d.AddForce(new Vector2(1, 0) * 100 * speed * Time.fixedDeltaTime);   
        
        if(moveLeft == false && position.x >= 74.36)
            moveLeft = true;
        else if(moveLeft == true && position.x <= 67.75)
            moveLeft = false;
        
    }

}
