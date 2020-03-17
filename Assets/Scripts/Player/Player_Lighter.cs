using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Lighter : MonoBehaviour
{

    private GameManager gm;
    private bool playMonologueOnce = false;

    public bool lighterOn = false;
    
    public int difficulty = 100;
    private int triesCounter;

    private void Start()
    {
        triesCounter = difficulty;
        gm = GetComponent<Player>().gm;
    }

    private void Update()
    {
        if(gm.holdPanel.transform.childCount != 0)
        {

            if(gm.holdPanel.transform.GetChild(0).GetComponent<Item_Inventory>().itemName == "lighter")
            {

                if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
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
                        UpdateAudio(1);
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

        if(Input.GetAxis("Mouse ScrollWheel") > 0.0f && lighterOn)
        {
            if (GameManager.Instance.inTutorial)
            {
                gm.TutorialNavi.UpdateBoard(7);
            }

            lighterOn = false;
        }

        gm.lighterObject.transform.GetChild(0).gameObject.SetActive(lighterOn);

        UpdateMonologue();
    }

    void TurnLighterOn(int chances)
    {
        int rng = Random.Range(0, chances);
        int hit = 0;

        if(hit == rng)
        {
            UpdateAudio(2);
            lighterOn = true;
            if (!gm.tutorialComplete)
            {
                gm.tutorialComplete = true;
                gm.TutorialNavi.gameObject.SetActive(false);
                gm.playerInventory.firstTime = false;
            }
            triesCounter = difficulty;

        }
        else
        {
            gm.lighterObject.GetComponentInChildren<ParticleSystem>().Play();
        }
    }

    void UpdateMonologue()
    {
        if (lighterOn && !playMonologueOnce)
        {
            playMonologueOnce = true;
            GameObject.Find("MonologueManager").GetComponent<MonologueManager>().DisplaySentence(0);
        }
        if(!lighterOn)
        {
            playMonologueOnce = false;
        }
    }

    void UpdateAudio(int index)
    {
        switch(index)
        {
            case 1:
                gm.audioManager.PlayAudio("lighter flick single");
                break;
            case 2:
                gm.audioManager.PlayAudio("lighter light up");
                break;
        }
    }
}
