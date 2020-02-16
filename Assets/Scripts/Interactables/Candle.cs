using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{

    public bool isLit = false;
    public Sprite spriteLit;
    public Sprite spriteNotLit;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(isLit==true)
        {
            
            if (other.CompareTag("Enemy"))
            {
                if(other.gameObject.GetComponent<MainGhost>().Chasing==false)
                {
                    Debug.Log("Snuff");
                    isLit = false;
                    GetComponent<SpriteRenderer>().sprite = spriteNotLit;
                    transform.GetChild(0).gameObject.SetActive(isLit);

                    if (other.gameObject.GetComponent<MainGhost>().enabled == true)
                    {
                        other.gameObject.GetComponent<MainGhost>().enabled = false;
                        
                        StartCoroutine(EnemyWake());

                    }
                }
                
            }
        }
        else
        {
            Debug.Log("nothing to snuff");
        }

        IEnumerator EnemyWake()
        {
            //This is a coroutine
            yield return new WaitForSeconds(2);    //Wait one frame
            GameObject.FindGameObjectWithTag("Enemy").GetComponent<MainGhost>().enabled = true;

        }
    }

}
