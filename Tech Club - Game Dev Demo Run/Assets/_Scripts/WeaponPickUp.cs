using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField] Weapon weapon;

    //This checks if the the pickup has collided with the player
    void OnTriggerEnter2D (Collider2D other)
    {
        //Simply calls the PickUpWeapon function with the weapon
        if (other.gameObject.tag.Equals ("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();

            pc.PickUpWeapon(weapon);

            Destroy(this.gameObject);
        }
    }
}
