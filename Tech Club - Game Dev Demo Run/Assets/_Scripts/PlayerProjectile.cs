using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    //Reference to the projectile's rigidbody2d
    [SerializeField] Rigidbody2D rb2d;

    //This is where the object's local center of mass will be
    [SerializeField] Vector3 centerOfMass;

    [SerializeField] int damage;

    // Start is called before the first frame update
    void Start()
    {
        //This sets the objects center of mass at given vector
        rb2d.centerOfMass = centerOfMass;
    }

    //Doing things depending on what they collide with.
    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            EnemyHealth em = other.gameObject.GetComponent<EnemyHealth>();
            Vector2 knockback = transform.position - other.transform.position;
            em.TakeDamage(damage, knockback);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag.Equals("Destructible"))
        {
            Destructible hitDes = other.gameObject.GetComponent<Destructible>();
            hitDes.GetDestroyed(transform.position);
            Destroy(this.gameObject);

        } else if (other.gameObject.tag.Equals("Ground"))
        {
            Destroy(this.gameObject);
        }
    }

    
}
