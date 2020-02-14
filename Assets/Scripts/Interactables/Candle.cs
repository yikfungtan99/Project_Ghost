using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{

    public bool isLit = false;

    public void LightCandle()
    {

        if(!isLit)
        {
            isLit = true;
        }

        transform.GetChild(0).gameObject.SetActive(isLit);
    }

}
