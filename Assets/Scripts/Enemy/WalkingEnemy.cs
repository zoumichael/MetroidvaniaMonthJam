using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject respawnObject;

    [SerializeField] private GameObject[] waypts;
    private int currentWayPoint = 0;
    public Animator animator;
    [SerializeField] private GameObject player;

    public bool aggroOntoPlayer = false;
    public bool resetting = false;

    [SerializeField] private float aggroRange;
    [SerializeField] private float tetherRange;
    [SerializeField] private float maxDistanceFromSpawn;

    private enum MovementState
    {
        IDLE,
        RUNNING
        //idle=0,running=1
    }
    private MovementState moveState = MovementState.IDLE;

    void Update()
    {
        
        if (resetting)
        {
            returnToRespawn();
        }
        else if(aggroOntoPlayer)
        {
            followPlayer();
        }
        else
        {
            moveToWaitPoint();
        }
        //animator.SetInteger("moveState", (int) state);
        GetComponent<Animator>().SetInteger("moveState", (int) moveState);
    }

    void followPlayer()
    {
        if (Vector2.Distance(respawnObject.transform.position, transform.position) > maxDistanceFromSpawn)
        {
            resetting = true;
            aggroOntoPlayer = false;
        }

        if (Vector2.Distance(player.transform.position, transform.position) > tetherRange)
        {

            Vector2 targetPosition = player.transform.position;
            targetPosition.y = transform.position.y;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);

            moveState = MovementState.RUNNING;
            flipSprite(targetPosition.x);
        }
        else
        {
            moveState = MovementState.IDLE;
        }
    }

    void returnToRespawn()
    {
        Debug.Log(Vector2.Distance(respawnObject.transform.position, transform.position));
        if (Vector2.Distance(respawnObject.transform.position, transform.position) < 1f)
        {
            Debug.Log("Reset");
            resetting = false;
        }
        Vector2 targetPosition = respawnObject.transform.position;
        targetPosition.y = transform.position.y;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
        flipSprite(targetPosition.x);

        moveState = MovementState.RUNNING;
    }

    void moveToWaitPoint()
    {
        if(Vector2.Distance(player.transform.position, transform.position) < aggroRange)
        {
            aggroOntoPlayer = true;
            return;
        }

        if (Vector2.Distance(waypts[currentWayPoint].transform.position, transform.position) < 1f)
        {
            currentWayPoint++;
            if (currentWayPoint >= waypts.Length)
            {
                currentWayPoint = 0;
            }
        }
        Vector2 targetPosition = waypts[currentWayPoint].transform.position;
        targetPosition.y = transform.position.y;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
        flipSprite(targetPosition.x);

        moveState = MovementState.RUNNING;
    }

    void flipSprite(float targetX)
    {
        if(transform.position.x < targetX)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
