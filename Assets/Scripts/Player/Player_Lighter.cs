using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Lighter : MonoBehaviour
{
    public bool lighterOn = false;

    public int difficulty = 100;
    private int triesCounter;

    private bool playOnce = false;

    private void Start()
    {
        triesCounter = difficulty;
    }

    private void Update()
    {
        if(transform.GetChild(2).GetChild(1).childCount != 0)
        {

            if(transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Item_Inventory>().itemName == "lighter")
            {

                if (Input.GetAxis("Mouse ScrollWheel") != 0)
                {

                    if (triesCounter <= 0)
                    {
                        triesCounter = 0;
                    }
                    else
                    {

                        triesCounter -= 1;

                    }

                    if (!lighterOn)
                    {
                        TurnLighterOn(triesCounter);
                    }
                }

            }
            else
            {
                lighterOn = false;
            }

        }
        else
        {
            lighterOn = false;
        }

        transform.GetChild(3).GetChild(0).gameObject.SetActive(lighterOn);

        if(lighterOn)
        {
            if(!playOnce)
            {
                playOnce = true;
                GameObject.Find("MonologueManager").GetComponent<MonologueManager>().DisplaySentence(0);
            }
        }
        else
        {
            playOnce = false;
        }
    }

    void TurnLighterOn(int chances)
    {
        int rng = Random.Range(0, chances);
        int hit = 0;

        if(hit == rng)
        {
            lighterOn = true;
            triesCounter = difficulty;

        }
        else
        {
            transform.GetChild(3).GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
        }
    }

}
