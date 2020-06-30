using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //This is how much we offset the camera from the player
    [SerializeField] Vector3 offset;


    //These values will determine the bounds of the camera
    [SerializeField] float xMinBounds;
    [SerializeField] float xMaxBounds;

    [SerializeField] float yMinBounds;
    [SerializeField] float yMaxBounds;

    //This references the player's transform component (position, rotation, scale)
    [SerializeField] Transform player;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    void FixedUpdate()
    {
        transform.position = Vector2.Lerp(player.position, transform.position, 0.3f);
        transform.position += offset;

    }
}
