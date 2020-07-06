using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]

    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    
    //If the player is standing on a ground
    [SerializeField] bool grounded;

    [SerializeField] Rigidbody2D rb2d; //This references the Rigidbody2D component of the player
    
    [SerializeField] float coyoteTime = 0; //This is a leeway that allows player to jump even after leaving ground by a few seconds. This gives room for player error.

    //This is an input buffer that counts down when the player presses space mid-air. 
    //If the player presses space right before they land, they should jump just as they land.
    [SerializeField] float jumpBuffer = 0;

    //If the player is facing right. Else, they're facing left.
    [SerializeField] bool facingRight = true;

    [Space(20)]
    [Header("Combat")]
    //This is the player's maximum health. Current health can't exceed this.
    [SerializeField] int maxHealth;
    //This is the player's current health. If it reaches 0, the player dies. It can't exceed maxHealth.
    [SerializeField] int currentHealth;

    //This is the all the weapons the player is carrying.
    [SerializeField] List<Weapon> weaponArsenal = new List<Weapon>();
    //This is the index of the player's current weapon.
    [SerializeField] int currentWeapon = 0;

    //This is where the player will attack from.
    [SerializeField] Transform attackPosition;

    //This value controls when the player can attack again after attacking.
    [SerializeField] float attackTimer;

    //Whenever the player is hit, they lose control until they land.
    [SerializeField] bool lossOfControl = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //FixedUpdate is better at handling physics than Update
    void FixedUpdate ()
    {
        //This is the player's horizontal movement.
        if (!lossOfControl)
            rb2d.AddForce(new Vector2(Input.GetAxisRaw("Horizontal"), 0) * 100 * speed * Time.fixedDeltaTime);        

        
    }

    //Jump
    void Jump ()
    {
        if (!lossOfControl)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);

            rb2d.AddForce(new Vector2(0, 100 * jumpPower));
            coyoteTime = 0;
            jumpBuffer = 0;
        }
    }

    // Update is called once per frame
    // The number of times the update function is called per second is dependant on the framerate
    void Update()
    {
        if (!grounded) //If the player isn't grounded, count down the coyote time.
        {
            coyoteTime -= 100f * Time.deltaTime; //Subtracting it by 100 per second.
            coyoteTime = Mathf.Clamp(coyoteTime, 0, 25f); //This just clamps the value between 0 and 15
        }
        
        //This just counts it down.
        jumpBuffer -= 100f * Time.deltaTime;
        jumpBuffer = Mathf.Clamp(jumpBuffer, 0, 25f);

        //To make the player jump more satisfying:
        //1) When the player is jumping the gravity on the player should be weaker.
        //2) If the player holding the space bar down while jumping, the gravity should be even weaker.
        //3) If the player is falling down, the gravity should be stronger.
        if (rb2d.velocity.y > 0 && Input.GetKey(KeyCode.Z))
            rb2d.gravityScale = 0.5f;
        else if (rb2d.velocity.y > 0)
            rb2d.gravityScale = 1.5f;
        else if (rb2d.velocity.y < 0)
            rb2d.gravityScale = 5f;

        //We place the attackPosition of the player based on their input.
        SetPlayerAttackPos();

        
        //This is called on the frame the space key is pressed. 
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if ((coyoteTime > 0 || grounded))
                Jump();
            else if (rb2d.velocity.y < 0) //If the player can't jump and is falling down, then buffer the input.
                jumpBuffer = 25f;
        }

        if (grounded && jumpBuffer > 0) //This is the jump input buffer.
        {
            Jump();
        }

        if (attackTimer > 0)
        {
            attackTimer -= 100 * Time.deltaTime;
            attackTimer = Mathf.Clamp(attackTimer, 0, Mathf.Infinity);
        }


        if (Input.GetKeyDown(KeyCode.X) && weaponArsenal.Count > 0 && attackTimer <= 0 && !lossOfControl)
        {
            attackTimer = weaponArsenal[currentWeapon].AttackTime;
            weaponArsenal[currentWeapon].UseWeapon(this, attackPosition.position);
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            CycleWeapon(true);
        } else if (Input.GetKeyDown(KeyCode.S))
        {
            CycleWeapon(false);
        }
    }

    void SetPlayerAttackPos()
    {

        switch (Input.GetAxisRaw("Horizontal"))
        {
            case 1:
                facingRight = true;
                break;
            case -1:
                facingRight = false;
                break;
            default:
                break;
        }

        switch (Input.GetAxisRaw("Vertical"))
        {
            case 1:
                attackPosition.localPosition = new Vector2(0, 1.5f);
                break;
            case -1:
                if (grounded)
                    attackPosition.localPosition = new Vector2(facingRight ? 1 : -1, -0.5f);
                else
                    attackPosition.localPosition = new Vector2(0, -1.5f);
                break;
            default:
                attackPosition.localPosition = new Vector2(facingRight ? 1 : -1, 0);
                break;
        }
    }

    //This sets if the player is grounded or not
    public void SetGrounded (bool _grounded)
    {
        grounded = _grounded;

        if (grounded)
        {
            coyoteTime = 15f; //Players have 0.15 second to jump after leaving the ground.
            lossOfControl = false;
        }
    }

    //Function for picking up weapons
    public void PickUpWeapon (Weapon weapon)
    {
        weaponArsenal.Add(weapon);
    }

    //This is just to allow the player to cycle between their weapons.
    void CycleWeapon (bool right)
    {
        if (weaponArsenal.Count > 0) {
            currentWeapon += right ? 1 : -1;
            if (currentWeapon >= weaponArsenal.Count)
                currentWeapon = 0;
            else if (currentWeapon < 0)
                currentWeapon = weaponArsenal.Count - 1;
        }
    }

    public void TakeDamage (int damage, Vector2 knockbackDirection)
    {
        currentHealth -= damage;

        lossOfControl = true;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        rb2d.AddForce(knockbackDirection * 1000);

        if (currentHealth <= 0)
            Die();
    }

    void Die ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Getters and Setters
    public Rigidbody2D RB2D
    {
        get
        {
            return rb2d;
        }
    }

    public bool Grounded
    {
        get
        {
            return grounded;
        }
    }

    public float AttackTimer
    {
        set
        {
            attackTimer = value;
        }
    }
}
