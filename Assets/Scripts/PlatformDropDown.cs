using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDropDown : MonoBehaviour
{
    [SerializeField] private float waitTimeBeforeFall;
    [SerializeField] private GameObject player;

    private Rigidbody2D playerRigidBody;

    private PlatformEffector2D pe;
    private float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        pe = GetComponent<PlatformEffector2D>();
        playerRigidBody = player.GetComponent<Rigidbody2D>();
    }
    void Awake()
    {
        pe = GetComponent<PlatformEffector2D>();
        playerRigidBody = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        checkFallDown();
        removeEdgeBounce();
    }

    void removeEdgeBounce()
    {
        if(playerRigidBody.velocity.y > 0f)
        {
            Debug.Log("0");
            pe.surfaceArc = 0f;
        }
        else
        {
            pe.surfaceArc = 180f;
        }
    }

    void checkFallDown()
    {
        if(Input.GetKeyUp(KeyCode.S))
        {
            waitTime = waitTimeBeforeFall;
        }
        if(Input.GetKey(KeyCode.S))
        {
            if(waitTime <= 0)
            {
                pe.rotationalOffset = 180f;
                waitTime = waitTimeBeforeFall;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            pe.rotationalOffset = 0f;
        }
    }
}
