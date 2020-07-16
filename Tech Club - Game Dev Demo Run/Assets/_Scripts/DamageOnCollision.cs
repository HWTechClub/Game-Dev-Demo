using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    //Amount of damage dealt
    [SerializeField] int damage;

    void OnTriggerEnter2D (Collider2D other)
    {
        //When this object collides with the player:
        if (other.gameObject.tag.Equals ("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();

            Vector2 knockback = pc.transform.position - transform.position;

            //Deal damage and knock them back
            pc.TakeDamage(damage, knockback.normalized);
        }
    }
    
}
