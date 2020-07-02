using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sword", menuName = "Weapons/Create new Sword", order = 1)]
public class Sword : Weapon
{
    [SerializeField] float damage;

    public override void UseWeapon(PlayerController player, Vector3 attackPos)
    {
        //Returns all colliders that overlap with this area.
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPos, 0.75f, this.layerMasks);

        foreach (Collider2D hit in hits)
        {
            if (!player.Grounded && Input.GetAxisRaw("Vertical") < 0)
            {
                player.RB2D.velocity = new Vector2(player.RB2D.velocity.x, 0);

                player.RB2D.AddForce(new Vector2(0, 100 * 7.5f));
            }
        }
    }
}
