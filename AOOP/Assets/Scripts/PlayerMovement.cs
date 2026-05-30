using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Global Script;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float walljumpcooldown;
    private float horizontalInput;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField]private AudioClip attackSound;
    [SerializeField]private AudioClip jumpSound;

    // This variable tracks if the player has collected the sword
    private bool gotsword;

    public GameObject swordRight;
    public GameObject swordLeft;


    private int facesight;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Initialize walljumpcooldown to avoid uninitialized usage
        walljumpcooldown = 0.5f; // Set initial cooldown value
        gotsword = false; // Initialize sword state
        facesight=1;
       
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Flip the character based on movement direction
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one; // Facing right

            facesight=1;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Facing left
            facesight=-1;
        }

        // Update animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
        anim.SetBool("sword", gotsword); // Update sword state in animator
        anim.SetBool("climbing", onWall());

        if (walljumpcooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            // Stick to the wall if the player is on a wall and not grounded
            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0; // Disable gravity to make the player stick
                body.velocity = Vector2.zero; // Stop the player from moving
            }
            else
            {
                body.gravityScale = 7; // Reset gravity if not on a wall
            }

            // Jump if the space key is pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
                //SoundManager.instance.PlaySound(jumpSound);
            }

            // Sword attack with the right mouse button
            if (Input.GetMouseButtonDown(1)) // Right mouse button is pressed
            {
                anim.SetTrigger("atk");
                if (gotsword)
                {
                    //SoundManager.instance.PlaySound(attackSound);
                }
               // SwordAttack();
            }
            //             else if (Input.GetMouseButtonUp(1)) // Right mouse button is pressed
            // {
            //     SwordAttackDone();
            // }

            // Check if the attack animation is playing
            // if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            // {
            //     // If the attack animation is playing, enable the sword
            //     SwordAttack();
            // }
            // else
            // {
            //     // Otherwise, disable the sword
            //     SwordAttackDone();
            // }
        }
        else
        {
            // Increment walljumpcooldown over time
            walljumpcooldown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            // Regular jump when grounded
            body.velocity = new Vector2(body.velocity.x, jumpForce);
            anim.SetTrigger("jump");
            Debug.Log("Jump triggered");
        }
        else if (onWall() && !isGrounded()) // Wall jump if on a wall but not grounded
        {
            // Wall jump logic
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 20, 1);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, jumpForce); // Push away from the wall
            }
            walljumpcooldown = 0; // Reset cooldown after wall jump
            Debug.Log("Wall jump triggered");
        }
    }

    private bool isGrounded()
    {
        // Check if the player is touching the ground using a BoxCast
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            Vector2.down,
            0.1f,
            groundLayer
        );

        return raycastHit.collider != null; // Return true if the player is grounded
    }

    private bool onWall()
    {
        // Adjusted BoxCast direction to check for walls based on the character's facing direction
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            new Vector2(transform.localScale.x, 0), // Cast slightly in the character's facing direction
            0.1f,
            wallLayer
        );
        return raycastHit.collider != null; // Return true if the player is on a wall
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object has the "sword" tag
        if (collision.gameObject.CompareTag("sword"))
        {
            // Set the sword state to true and destroy the sword object
            gotsword = true;
            Destroy(collision.gameObject); // Destroy the sword prefab
            Debug.Log("Sword collected!");
        }
    }

    public void SwordAttack()
    {
        //if (facesight==1)
            swordRight.SetActive(true);
        //else if (facesight==-1)
            //swordLeft.SetActive(true);

        Debug.Log("SwordAttack triggered");
    }

    public void SwordAttackDone()
    {
        //if (facesight == 1)
            swordRight.SetActive(false);
        //else if (facesight == -1)
            //swordLeft.SetActive(false);

        Debug.Log("SwordAttackDone triggered");
    }
}
