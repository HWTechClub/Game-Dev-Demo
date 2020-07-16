using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] BoxCollider2D collider2D;

    //Checks if the player is in contact with the ground.

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag.Equals ("Ground"))
        {
            controller.SetGrounded(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Ground"))
        {
            controller.SetGrounded(false);
        }
    }

    void Update ()
    {
        if (controller.RB2D.velocity.y != 0)
        {
            collider2D.size = new Vector2(0.8f, 0.1f);
        }
        else
        {
            collider2D.size = new Vector2(0.9f, 0.1f);
        }
    }
}
