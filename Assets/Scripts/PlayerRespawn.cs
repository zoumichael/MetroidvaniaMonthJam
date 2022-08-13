using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private float respawnX;
    [SerializeField] private float respawnY;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Danger"))
        {
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        rb.transform.position = new Vector3(respawnX, respawnY, 0);
    }

    public void SetRespawnX(float val) { respawnX = val; }
    public void SetRespawnY(float val) { respawnY = val; }

}
