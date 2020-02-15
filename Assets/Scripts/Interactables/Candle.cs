using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{

    public bool isLit = false;
    public Sprite spriteLit;

    public void LightCandle()
    {

        if(!isLit)
        {
            isLit = true;
        }

        if (isLit)
        {
            GetComponent<SpriteRenderer>().sprite = spriteLit;
        }

        transform.GetChild(0).gameObject.SetActive(isLit);
    }

}
