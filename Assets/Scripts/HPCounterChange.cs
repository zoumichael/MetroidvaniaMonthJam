using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPCounterChange : MonoBehaviour
{
    public Sprite FULLHP, EMPTYHP;
    public Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
    }

    public void SetFullHP()
    {
        image.sprite = FULLHP;
    }

    public void SetEmptyHP()
    {
        image.sprite = EMPTYHP;
    }
}
