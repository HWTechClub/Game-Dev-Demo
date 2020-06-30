using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    
    //If the player is standing on a ground
    [SerializeField] bool grounded;

    [SerializeField] Rigidbody2D rb2d; //This references the Rigidbody2D component of the player

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate ()
    {
        
        rb2d.AddForce(new Vector2(Input.GetAxisRaw("Horizontal"), 0) * 100 * speed * Time.fixedDeltaTime);

        //This is called on the frame the space key is pressed. This is the jump.
        if (Input.GetKey(KeyCode.Space) && grounded && rb2d.velocity.y == 0)
        {
            rb2d.AddForce(new Vector2(0, 100 * jumpPower));
        }

        //To make the player jump more satisfying:
        //1) When the player is jumping the gravity on the player should be weaker.
        //2) If the player holding the space bar down while jumping, the gravity should be even weaker.
        //3) If the player is falling down, the gravity should be stronger.
        if (rb2d.velocity.y > 0 && Input.GetKey(KeyCode.Space))
            rb2d.gravityScale = 0.5f;
        else if (rb2d.velocity.y > 0)
            rb2d.gravityScale = 1.5f;
        else if (rb2d.velocity.y < 0)
            rb2d.gravityScale = 3f;
    }

    // Update is called once per frame
    // The number of times the update function is called per second is dependant on the framerate
    void Update()
    {
        
        
    }


    //This sets if the player is grounded or not
    public void SetGrounded (bool _grounded)
    {
        grounded = _grounded;
    }
}
