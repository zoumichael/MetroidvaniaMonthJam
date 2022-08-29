using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMakePlatform : MonoBehaviour
{
    // GLOBAL VARIABLE USED TO DETERMINE IF THE PLAYER CAN SPAWN A PLATFORM OR NOT.
    public static bool CanGerminate = true;

    public void SetCanGerminate(bool val)
    {
        CanGerminate = val;
    }

    // Platform to Create.
    public GameObject platformPrefab;

    // Variables used to control how long it takes for the platform to spawn.
    [SerializeField] private float waitTimeBeforeRegerminate;
    private float waitTime;

    // Gap between 
    [SerializeField] float spawnPlatformYOffset;

    // All types of ground you can make the platform one. 
    [SerializeField] private LayerMask jumpableGround;

    private void Update()
    {
        if (CanGerminate)
        {
            Regerminate();
        }
    }

    public void Regerminate()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            waitTime = waitTimeBeforeRegerminate;
        }
        if (Input.GetKey(KeyCode.K) && IsGrounded())
        {
            if (waitTime <= 0)
            {
                waitTime = waitTimeBeforeRegerminate;
                Vector3 platformSpawnLocation = new Vector3(
                                                        transform.position.x,
                                                        transform.position.y + spawnPlatformYOffset,
                                                        transform.position.z);
                Instantiate(platformPrefab, platformSpawnLocation, Quaternion.identity);
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    public void ResetGerminateCounter()
    {
        waitTime = waitTimeBeforeRegerminate;
    }

    private bool IsGrounded()
    {
        BoxCollider2D coll = GetComponent<BoxCollider2D>();
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
