using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //This is the enemy's maximum health. Current health can't exceed this.
    [SerializeField] int maxHealth;
    //This is the enemy's current health. If it reaches 0, the enemy dies. It can't exceed maxHealth.
    [SerializeField] int currentHealth;

    [SerializeField] Rigidbody2D rb2d;

    public void TakeDamage(int damage, Vector2 knockbackDirection)
    {
        currentHealth -= damage;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        rb2d.AddForce(-knockbackDirection * 400);

        if (currentHealth <= 0)
            Destroy(this.gameObject);
    }

}
