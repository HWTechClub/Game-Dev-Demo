using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField] Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This checks if the the pickup has collided with the player
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag.Equals ("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();

            pc.PickUpWeapon(weapon);

            Destroy(this.gameObject);
        }
    }
}
