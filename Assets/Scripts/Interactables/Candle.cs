using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    private bool playMonologueOnce = false;

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

        UpdateMonologue();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(isLit==true)
        {
            
            if (other.CompareTag("Enemy"))
            {
                if(other.gameObject.GetComponent<CarrotMain>().anima.GetBool("isChase") == false)
                {
                    isLit = false;
                    other.gameObject.GetComponent<CarrotMain>().anima.SetBool("isIdle", true);
                    other.gameObject.GetComponent<CarrotMain>().anima.SetBool("isPatrol", false);
                    StartCoroutine(EnemyWake());

                }
                /*if(other.gameObject.GetComponent<MainGhost>().Chasing==false)
                {
                    
                    isLit = false;


                    if (other.gameObject.GetComponent<MainGhost>().enabled == true)
                    {
                        other.gameObject.GetComponent<MainGhost>().enabled = false;
                        
                        StartCoroutine(EnemyWake());

                    }
                }*/
                
            }
        }
        else
        {
            Debug.Log("nothing to snuff");
        }

        IEnumerator EnemyWake()
        {
            //This is a coroutine
            yield return new WaitForSeconds(1);    //Wait one frame
            GetComponent<SpriteRenderer>().sprite = spriteNotLit;
            transform.GetChild(0).gameObject.SetActive(isLit);
            other.gameObject.GetComponent<CarrotMain>().anima.SetBool("isPatrol", true);
            other.gameObject.GetComponent<CarrotMain>().anima.SetBool("isChase", false);
            other.gameObject.GetComponent<CarrotMain>().anima.SetBool("isIdle", false);

            GetComponent<SpriteRenderer>().sprite = spriteNotLit;
            transform.GetChild(0).gameObject.SetActive(isLit);


        }
    }

    void UpdateMonologue()
    {
        if(isLit && !playMonologueOnce)
        {
            playMonologueOnce = true;
            GameObject.Find("MonologueManager").GetComponent<MonologueManager>().DisplaySentence(1);
        }
        if(!isLit)
        {
            playMonologueOnce = false;
        }
    }
}
