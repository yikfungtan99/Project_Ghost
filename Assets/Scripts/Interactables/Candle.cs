using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    private bool playOnce = false;
    public bool isLit = false;
    public Sprite spriteLit;

    private void Update()
    {
        
        if(!isLit)
        {
            playOnce = false;
        }
    }
    public void LightCandle()
    {

        if(!isLit)
        {
            isLit = true;
        }

        if (isLit)
        {
            GetComponent<SpriteRenderer>().sprite = spriteLit;

            if(!playOnce)
            {
                playOnce = true;
                GameObject.Find("MonologueManager").GetComponent<MonologueManager>().DisplaySentence(3);
            }
        }

        transform.GetChild(0).gameObject.SetActive(isLit);
    }

}
