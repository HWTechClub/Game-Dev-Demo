using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bow", menuName = "Weapons/Create new Bow", order = 1)]
public class Bow : Weapon
{
    [SerializeField] GameObject arrow;

    public override void UseWeapon(PlayerController player, Vector3 attackPos)
    {
        //If the player is falling, smoothen it a bit.
        if (player.RB2D.velocity.y < 0)
            player.RB2D.velocity = new Vector2(player.RB2D.velocity.x, 0);

        //Create an arrow object.
        GameObject _arrow =  Instantiate(arrow, attackPos, arrow.transform.rotation);

        Rigidbody2D rb2d = _arrow.GetComponent<Rigidbody2D>();

        Vector2 angle = attackPos - player.transform.position;

        //Change the rotation of the arrow to face the direction it is fired.
        _arrow.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg);

        //Add force to the arrow.
        rb2d.AddForce(angle.normalized * 2000);
    }
}
