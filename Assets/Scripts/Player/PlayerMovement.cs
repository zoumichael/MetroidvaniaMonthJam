using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    
    [SerializeField] private LayerMask jumpableGround;

    [SerializeField] private float jumpSpeed = 14f;
    [SerializeField] private float moveSpeed = 7f;

    [SerializeField] private AudioSource jumpSoundEffect;

    [SerializeField] private static int maxJumps = 1;
    public int currJump = 1;
    public Animator animator;
    [SerializeField] private float airborneMovespeed;

    private bool isGliding;
    static bool canGlide = true;
    [SerializeField] private float glideDropSpeed;
    [SerializeField] private float normalDropSpeed;

    // After taking damage, lose control of the player for a while.
    [SerializeField] private float damageLockOut;
    private float damageLockOutCounter;

    private enum MovementState
    {
        IDLE,
        RUNNING,
        JUMPING,
        FALLING,
        GLIDING
        //idle=0,running=1,jumping=2,falling=3,glide
    }
    private MovementState moveState = MovementState.IDLE;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        damageLockOutCounter = -1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(damageLockOutCounter < 0)
        {
            UpdateMovement();
        }
        else
        {
            damageLockOutCounter -= Time.deltaTime;
        }
        UpdateAnimation();
    }

    public void damaged()
    {
        damageLockOutCounter = damageLockOut;
    }

    private void UpdateMovement()
    {
        MovementState state;
        float dirX = Input.GetAxisRaw("Horizontal");
        if(dirX > 0f)
        {
            state = MovementState.RUNNING;
            sprite.flipX = false;

        }
        else if(dirX < 0f)
        {
            state = MovementState.RUNNING;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.IDLE;
        }
        // Need different horizontal movement speeds if you are in the air and not gliding. 
        if(IsGrounded() || isGliding)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }
        else
        {
            float newMoveSpeed = rb.velocity.x + dirX * moveSpeed * airborneMovespeed;
            if (newMoveSpeed > moveSpeed)
                newMoveSpeed = moveSpeed;
            else if (newMoveSpeed < -moveSpeed)
                newMoveSpeed = -moveSpeed;
            rb.velocity = new Vector2(newMoveSpeed, rb.velocity.y);
        }

        // Jump 
        if (Input.GetKeyDown(KeyCode.W) && currJump > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            currJump--;
            isGliding = false;
        }
        // Reset jump counter when you land on the ground.
        if(currJump < maxJumps && IsGrounded())
        {
            currJump = maxJumps;
        }

        // Handle Gliding Mechanics
        if(Input.GetKeyDown(KeyCode.F) && !IsGrounded() && canGlide)
        {
            isGliding = true;
        }
        if(!Input.GetKey(KeyCode.F))
        {
            isGliding = false;
        }

        // Hard cap on fall speed
        if (isGliding)
        {
            if(rb.velocity.y < glideDropSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, glideDropSpeed);
            }
        }
        else
        {
            if (rb.velocity.y < normalDropSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, normalDropSpeed);
            }
        }
        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.JUMPING;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.FALLING;
            if (isGliding)
            {
                state = MovementState.GLIDING;
            }
        }
        animator.SetInteger("moveState", (int) state);
    }

    private void UpdateAnimation()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        if (dirX > 0f)
        {
            sprite.flipX = false;

        }
        else if (dirX < 0f)
        {
            sprite.flipX = true;
        }
        else
        {
        }
        if (rb.velocity.y > 0.1f)
        {
        }
        else if (rb.velocity.y < -0.1f)
        {
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

}
