using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    // Boss HP
    [SerializeField] private float maxHP;
    public float currHP;
    [SerializeField] private float hpThreshold1;
    [SerializeField] private float hpThreshold2;

    // Boss Attack Parameters
    private float attackCounter;
    [SerializeField] private float attackCounterMax;

    // Dash Attack
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    private bool dashing = false;

    // Player
    [SerializeField] private GameObject player;

    // Other Components
    Rigidbody2D rb;

    // Spit Attack
    [SerializeField] private GameObject spitAttack;

    private enum MovementState
    {
        IDLE,
        SPIT,
        DASH
        //idle=0,spit=1,dash=2
    }
    private MovementState moveState = MovementState.IDLE;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if(currHP < hpThreshold1)
        {
            UpdateLowHP();
        }
        else if(currHP < hpThreshold2)
        {
            UpdateMidHP();
        }
        else
        {
            UpdateHighHP();
        }
    }

    void UpdateLowHP()
    {
        attackCounter += Time.deltaTime;
        if(attackCounter > attackCounterMax)
        {
            attackCounter = 0;
        }
    }

    void UpdateMidHP()
    {
        attackCounter += Time.deltaTime;
        if (attackCounter > attackCounterMax)
        {
            attackCounter = 0;
            
        }
    }

    void UpdateHighHP()
    {
        
        attackCounter += Time.deltaTime; 
        if (!dashing && attackCounter > attackCounterMax)
        {
            int attackType = Random.Range(0, 2);

            if(attackType == 0)
            {
                DashTowardsPlayer();
                attackCounter = 0;
            }
            else if(attackType == 1)
            {
                SpitAttack();
                attackCounter = 0;
            }
        }

        if (attackCounter > dashTime)
        {
            StopDashing();
            attackCounter = 0;
        }
    }

    void DashTowardsPlayer()
    {
        dashing = true;
        if(player.transform.position.x > transform.position.x)
        {
            Debug.Log("Dash Right. Player: " + player.transform.position.x + " Me: " + transform.position.x);
            rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
        }
        else
        {
            Debug.Log("Dash Left");
            rb.velocity = new Vector2(-dashSpeed, rb.velocity.y);
        }
    }

    void StopDashing()
    {
        dashing = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    void SpitAttack()
    {
        Vector3 spitSpawnLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject temp = Instantiate(spitAttack, spitSpawnLocation, Quaternion.identity);
        if (player.transform.position.x > transform.position.x)
        {
            temp.GetComponent<BossSpitMovement>().setTrajectory(10f, 5f);
        }
        else
        {
            temp.GetComponent<BossSpitMovement>().setTrajectory(-10f, 5f);
        }
            
    }
}
