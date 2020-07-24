using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wand", menuName = "Weapons/Create new Wand", order = 1)]
public class Wand : Weapon
{
    [SerializeField] GameObject magicBall;

    public override void UseWeapon(PlayerController player, Vector3 attackPos)
    {
        //If the player is falling, smoothen it a bit.
        if (player.RB2D.velocity.y < 0)
            player.RB2D.velocity = new Vector2(player.RB2D.velocity.x, 0);

        //Create an ball object.
        GameObject ball = Instantiate(magicBall, attackPos, magicBall.transform.rotation);

        Rigidbody2D rb2d = ball.GetComponent<Rigidbody2D>();

        Vector2 angle = attackPos - player.transform.position;

        //Add force to the ball.
        rb2d.AddForce(angle.normalized * 1000);

        Destroy(ball, 5f);
    }
}
