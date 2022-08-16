using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private LayerMask jumpableGround;

    [SerializeField] private float respawnX;
    [SerializeField] private float respawnY;

    private BoxCollider2D coll;
    private Rigidbody2D rb;

    [SerializeField] private float waitTimeBeforeRegerminate;
    private float waitTime;

    public GameObject platformPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Regerminate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Danger"))
        {
            RespawnPlayer();
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
                Instantiate(platformPrefab, transform.position, Quaternion.identity);
                RespawnPlayer();
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    public void RespawnPlayer()
    {
        rb.transform.position = new Vector3(respawnX, respawnY, 0);
    }

    public void SetRespawnX(float val) { respawnX = val; }
    public void SetRespawnY(float val) { respawnY = val; }

}
