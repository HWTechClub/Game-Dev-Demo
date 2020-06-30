using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] PlayerController controller;

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

}
