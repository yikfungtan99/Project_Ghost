﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : Interactable
{
    private bool playMonologueOnce = false;

    public bool isLit = false;
    public Sprite spriteLit;
    public Sprite spriteNotLit;

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

        UpdateMonologue();
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
