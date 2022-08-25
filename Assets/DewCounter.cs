using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DewCounter : MonoBehaviour
{
    public static int DewCount = 0;

    private void Update()
    {
        GetComponent<TextMeshProUGUI>().SetText("Dew: " + DewCount);
    }

    public void RemoveDew(int value)
    {
        DewCount -= value;
        if(DewCount < 0)
        {
            DewCount = 0;
        }
        UpdateText();
    }
    public void AddDew(int value)
    {
        DewCount += value;
        UpdateText();
    }

    private void UpdateText()
    {
        Debug.Log("New Dew Value:" + DewCount);
    }
}
