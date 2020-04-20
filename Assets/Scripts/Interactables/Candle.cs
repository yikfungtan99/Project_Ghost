using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : Interactable
{
    private bool playMonologueOnce = false;

    public bool isLit = false;
    public Sprite spriteLit;
    public Sprite spriteNotLit;
    public bool LightOnStart = false;
    public void Start()
    {
        if(LightOnStart==true)
        {
            LightCandle();
        }    
       
    }
    public override void Interact()
    {
 
        LightCandle();

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
        }

        transform.GetChild(0).gameObject.SetActive(isLit);

        if(LightOnStart == false)
        {
            UpdateAudio(1);
            UpdateMonologue(-1, "");
        }
        
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {

        if (isLit == true)
        {

            if (collision.CompareTag("Enemy") && collision.GetComponent<CarrotMain>().anima.GetBool("isLight") == true)
            {
                if (collision.gameObject.GetComponent<CarrotMain>().anima.GetBool("isChase") == false)
                {
                    isLit = false;
                    collision.gameObject.GetComponent<CarrotMain>().anima.SetBool("isIdle", true);
                    collision.gameObject.GetComponent<CarrotMain>().anima.SetBool("isPatrol", false);
                    collision.gameObject.GetComponent<CarrotMain>().anima.SetBool("isLight", false);
                    StartCoroutine(EnemyWake());

                }
            }
            else
            {
                //Debug.Log("nothing to snuff");
            }

            IEnumerator EnemyWake()
            {
                yield return new WaitForSeconds(1);    //Wait one frame
                GetComponent<SpriteRenderer>().sprite = spriteNotLit;
                transform.GetChild(0).gameObject.SetActive(isLit);
                collision.gameObject.GetComponent<CarrotMain>().anima.SetBool("isPatrol", true);
                collision.gameObject.GetComponent<CarrotMain>().anima.SetBool("isChase", false);
                collision.gameObject.GetComponent<CarrotMain>().anima.SetBool("isIdle", false);
                collision.gameObject.GetComponent<CarrotMain>().anima.SetBool("isLight", false);

                GetComponent<SpriteRenderer>().sprite = spriteNotLit;
                transform.GetChild(0).gameObject.SetActive(isLit);


            }
        }
    }

    public override void UpdateMonologue(int displayIndex, string itemName)
    {
        if(isLit && !playMonologueOnce)
        {
            playMonologueOnce = true;
            gm.monologueManager.DisplaySentence(1);
        }
        if(!isLit)
        {
            playMonologueOnce = false;
        }
    }

    public override void UpdateAudio(int index)
    {
        switch(index)
        {
            case 1:
                gm.audioManager.ForcePlayAudio("candle flame");
                break;
        }
    }
}
