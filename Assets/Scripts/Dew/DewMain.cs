using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DewMain : MonoBehaviour
{
    [SerializeField] GameObject dewCounter;

    [SerializeField] private int value;

    public void InitializeDew(int v, float x, float y)
    {
        value = v;
        GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            dewCounter.GetComponent<DewCounter>().AddDew(value);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dewCounter.GetComponent<DewCounter>().AddDew(value);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Terrain"))
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            if (collision.transform.position.y < transform.position.y)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
            else if (collision.transform.position.y < transform.position.y)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            }
        }
    }
}
