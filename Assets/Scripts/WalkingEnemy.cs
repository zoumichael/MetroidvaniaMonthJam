using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] waypts;
    private int currentWayPoint = 0;

    [SerializeField] private float speed = 2f;

    void Update()
    {
        if (Vector2.Distance(waypts[currentWayPoint].transform.position, transform.position) < .1f)
        {
            currentWayPoint++;
            if (currentWayPoint >= waypts.Length)
            {
                currentWayPoint = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypts[currentWayPoint].transform.position, Time.deltaTime * speed);
    }
}
