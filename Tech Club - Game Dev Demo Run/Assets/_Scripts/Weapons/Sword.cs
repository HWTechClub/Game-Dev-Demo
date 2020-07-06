using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sword", menuName = "Weapons/Create new Sword", order = 1)]
public class Sword : Weapon
{
    [SerializeField] int damage;

    public override void UseWeapon(PlayerController player, Vector3 attackPos)
    {
        //Returns all colliders that overlap with this area.
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPos, 0.75f, Masks);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.tag.Equals ("Enemy"))
            {
                //Do damage
            } else if (hit.gameObject.tag.Equals("Destructible"))
            {
                Destructible hitDes = hit.GetComponent<Destructible>();
                hitDes.GetDestroyed(player.transform.position);
            }
        }

        if (!player.Grounded && Input.GetAxisRaw("Vertical") < 0 && hits.Length > 0)
        {
            player.RB2D.velocity = new Vector2(player.RB2D.velocity.x, 0);

            player.RB2D.AddForce(new Vector2(0, 100 * 7.5f));
        }
    }
}
