using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterTime : MonoBehaviour
{
    [SerializeField] float deleteTime;
    float counter = 0;

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(deleteTime < counter)
        {
            Destroy(gameObject);
        }
    }
}
