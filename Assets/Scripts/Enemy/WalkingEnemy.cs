using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject respawnObject;

    [SerializeField] private GameObject[] waypts;
    private int currentWayPoint = 0;

    [SerializeField] private GameObject player;

    public bool aggroOntoPlayer = false;
    public bool resetting = false;

    [SerializeField] private float aggroRange;
    [SerializeField] private float tetherRange;
    [SerializeField] private float maxDistanceFromSpawn;

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
        }
    }

    void returnToRespawn()
    {
        if (Vector2.Distance(respawnObject.transform.position, transform.position) < .1f)
        {
            resetting = false;
        }
        Vector2 targetPosition = respawnObject.transform.position;
        targetPosition.y = transform.position.y;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
    }

    void moveToWaitPoint()
    {
        if(Vector2.Distance(player.transform.position, transform.position) < aggroRange)
        {
            aggroOntoPlayer = true;
            return;
        }

        if (Vector2.Distance(waypts[currentWayPoint].transform.position, transform.position) < .1f)
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
    }
}
