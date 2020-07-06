using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    [SerializeField] int damage;

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag.Equals ("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();

            Vector2 knockback = pc.transform.position - transform.position;

            pc.TakeDamage(damage, knockback.normalized);
        }
    }
}
