using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpitMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private float destroyAfter;

    private enum MovementState
    {
        GOINGUP,
        GOINGDOWN,
        ONTHEGROUND
    }
    MovementState moveState = MovementState.GOINGUP;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        updateMovementState();
        destroyAfter -= Time.deltaTime;
        if(destroyAfter < 0)
        {
            Destroy(gameObject);
        }
    }

    public void setTrajectory(float hspeed, float vspeed)
    {
        rb.velocity = new Vector2(hspeed, vspeed);
    }

    public void updateMovementState()
    {
        if (rb.velocity.y > 0.5f)
        {
            moveState = MovementState.GOINGUP;
        }
        else if(rb.velocity.y < -0.5f)
        {
            moveState = MovementState.GOINGDOWN;
        }
        else
        {
            if(moveState == MovementState.GOINGDOWN)
            {
                Debug.Log("landed");
                moveState = MovementState.ONTHEGROUND;
                rb.velocity = new Vector2(0, 0);
            }
        }
        //animator.SetInteger("moveState", (int) moveState);
    }
}
