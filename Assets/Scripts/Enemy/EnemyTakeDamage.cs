using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    [SerializeField] private float recoil;

    [SerializeField] private float maxHP;
    private float currHP;

    [SerializeField] private float timeUntilReset;

    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        currHP = maxHP;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            takeDamage(10.0f, collision);
            GetComponent<SpawnDew>().SpawnRandomDew(1);
            Destroy(collision);
        }
    }

    private void takeDamage(float dmg, Collider2D collision)
    {
        Debug.Log("Enemy Hit");
        currHP -= dmg;
        if(maxHP < 0)
        {

        }
        else
        {
            if (recoil > 0.0f)
            {
                // If XOffset or YOffset is postiive it means that the collision came from the left/down
                float XOffset = transform.position.x - collision.transform.position.x;
                float YOffset = transform.position.y - collision.transform.position.y;

                rb.velocity = new Vector3(XOffset*recoil, YOffset*recoil, 0);
            }
        }
    }
}
