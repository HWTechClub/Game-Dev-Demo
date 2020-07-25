using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            Vector2 knockback = other.transform.position - transform.position;
            player.TakeDamage(1, knockback);
            Destroy(this.gameObject);
        }

        else if (other.gameObject.tag.Equals("Destructible"))
        {
            Destructible hitDes = other.gameObject.GetComponent<Destructible>();
            hitDes.GetDestroyed(transform.position);
            Destroy(this.gameObject);

        } 
        
        else if (other.gameObject.tag.Equals("Ground"))
        {
            Destroy(this.gameObject);
        }
    }

}
