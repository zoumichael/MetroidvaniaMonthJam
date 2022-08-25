using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDew : MonoBehaviour
{
    public GameObject dewPrefab;

    [SerializeField] private float dewInitialY;
    [SerializeField] private float dewXRange;

    public void SpawnRandomDew(int val)
    {
        GameObject newDew = Instantiate(dewPrefab, transform.position, Quaternion.identity);
        newDew.GetComponent<DewMain>().InitializeDew(val, Random.Range(-dewXRange, dewXRange), dewInitialY);
    }
}
