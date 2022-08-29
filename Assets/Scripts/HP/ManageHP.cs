using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageHP : MonoBehaviour
{
    [SerializeField] static List<GameObject> healthCounter = new List<GameObject>();
    [SerializeField] float HPLeft;
    [SerializeField] float HPOffset;

    public static int maxHP;
    public static int currentHp;

    public GameObject hpPrefab;
    public Sprite FULLHP, EMPTYHP;

    // Start is called before the first frame update
    void Start()
    {

        maxHP = 5;
        currentHp = maxHP;
        for (int i = 0; i<maxHP; i++)
        {
            Vector3 hpSpawnLocation = new Vector3(
                                                        transform.position.x + HPLeft + HPOffset * i,
                                                        transform.position.y,
                                                        transform.position.z);
            Debug.Log("Added HP Counter at " + hpSpawnLocation);
            healthCounter.Add(Instantiate(hpPrefab, hpSpawnLocation, Quaternion.identity));
            healthCounter[i].transform.SetParent(transform);
            healthCounter[i].GetComponent<HPCounterChange>().SetFullHP();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseMaxHP()
    {

    }

    public bool takeDamage(int value = 1)
    {
        currentHp -= value;
        Debug.Log("Current HP: " + currentHp);
        for(int i = currentHp; i<currentHp + value; i++)
        {
            healthCounter[i].GetComponent<HPCounterChange>().SetEmptyHP();
        }
        if(currentHp <= 0)
        {
            return true;
        }
        return false;
    }

    public void healToFull()
    {
        currentHp = maxHP;
        Debug.Log("Current HP: " + currentHp);
        foreach (GameObject go in healthCounter)
        {
            go.GetComponent<HPCounterChange>().SetFullHP();
        }
    }

    public int getCurrHP()
    {
        return currentHp;
    }


}
