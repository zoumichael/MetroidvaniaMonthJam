using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAndDamage : MonoBehaviour
{
    private GameObject attack;
    public GameObject meleeAttackPrefab;

    private bool facingRight = true;
    [SerializeField] private float meleeAttackRange = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkAttack();
        updateDirection();
    }

    void updateDirection()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        if (dirX > 0f)
        {
            facingRight = true;
            meleeAttackRange = Mathf.Abs(meleeAttackRange);

        }
        else if (dirX < 0f)
        {
            facingRight = false;
            meleeAttackRange = - Mathf.Abs(meleeAttackRange);
        }
    }

    void checkAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    void createAttack()
    {
        Vector3 attackSpawnLocation = new Vector3(transform.position.x + meleeAttackRange, transform.position.y, transform.position.z);
        attack = Instantiate(meleeAttackPrefab, attackSpawnLocation, Quaternion.identity);
    }

    void destroyAttack()
    {
        if(attack != null)
        {
            Destroy(attack);
        }

    }
}
