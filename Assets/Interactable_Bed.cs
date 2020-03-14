﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Bed : Interactable
{

    public Tutorial_Triggers[] TriggersToSet;
    public bool ableToSleep = false;
    public GameObject FadeInOut;

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (ableToSleep)
        {

            FadeInOut.SetActive(true);

        }
    }

    public override void Interact()
    {
        base.Interact();

        TutorialFadeInOut();

    }

    public void TutorialFadeInOut()
    {

        if (ableToSleep)
        {
            

            for (int i = 0; i < TriggersToSet.Length; i++)
            {

                TriggersToSet[i].isEnabled = true;

            }

            if (!gm.tutorialSleep)
            {
                gm.FadeInOutAnim.SetBool("Go", true);
                StartCoroutine(Delay(2));
                
            }

        }
        else
        {
            //Insert Monologue Here
            Debug.Log("I should light up the candle");

        }

    }

    IEnumerator Delay(float waitForSeconds)
    {
        gm.playerLighter.lighterOn = false;
        gm.playerObject.SetActive(false);
        yield return new WaitForSeconds(waitForSeconds);

        gm.playerObject.SetActive(true);
        ableToSleep = false;
        FadeInOut.SetActive(false);
        gm.tutorialSleep = true;
        
    }

}