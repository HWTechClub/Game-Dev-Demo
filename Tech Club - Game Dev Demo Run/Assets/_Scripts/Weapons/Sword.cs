using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sword", menuName = "Weapons/Create new Sword", order = 1)]
public class Sword : Weapon
{
    [SerializeField] int damage;

    public override void UseWeapon(PlayerController player, Vector3 attackPos)
    {
        //If the player is falling, smoothen it a bit.
        if (player.RB2D.velocity.y < 0)
            player.RB2D.velocity = new Vector2(player.RB2D.velocity.x, 0);
        //Returns all colliders that overlap with this area.
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPos, .75f, Masks);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.tag.Equals ("Enemy"))
            {
                //If its an enemy, damage it.
                EnemyHealth em = hit.gameObject.GetComponent<EnemyHealth>();
                Vector2 knockback = player.gameObject.transform.position - hit.transform.position;
                em.TakeDamage(damage, knockback);
            } else if (hit.gameObject.tag.Equals("Destructible"))
            {
                //If its a destructible, destroy it.
                Destructible hitDes = hit.GetComponent<Destructible>();
                hitDes.GetDestroyed(player.transform.position);
            }
        }

        //If the player is airborne and strikes and object below them, launch them upwards.
        if (!player.Grounded && Input.GetAxisRaw("Vertical") < 0 && hits.Length > 0)
        {
            player.RB2D.velocity = new Vector2(player.RB2D.velocity.x, 0);

            player.RB2D.AddForce(new Vector2(0, 100 * 7.5f));
        }
    }
}
