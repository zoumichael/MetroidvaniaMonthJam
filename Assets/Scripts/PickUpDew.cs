using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dew : MonoBehaviour
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
}
