using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathLoad : MonoBehaviour
{

    GameManager gm;
    public bool forTutorial = false;
    public Tutorial_Triggers resetTrigger;
    public Tutorial_Triggers resetTrigger2;
    public Tutorial_Candle tutorialCandle;
    private Vector2 ghostInitPos;

    private void Start()
    {
        gm = GameManager.Instance;

        if(gm.ghostMain != null)
		{
			ghostInitPos = gm.ghostMain.transform.position;
		}

    }

    [System.Obsolete]
    public void Restart()
    {
		UpdateAudio(1);

        if (forTutorial)
        {
            //Application.LoadLevel(Application.loadedLevel);

            gm.playerObject.transform.position = gm.playerCheckpointPosition;
            gm.playerObject.SetActive(true);
            gm.player.isDead = false;

            gm.ghostMain.transform.position = ghostInitPos;
            gm.ghostMain.SetActive(false);

            if (resetTrigger)
            {
                resetTrigger.isEnabled = true;
            }

            if (resetTrigger2)
            {
                resetTrigger2.ready = false;
            }

            if (!tutorialCandle.isLit)
            {

                tutorialCandle.isLit = true;
                tutorialCandle.GetComponent<SpriteRenderer>().sprite = tutorialCandle.spriteLit;
                tutorialCandle.transform.GetChild(0).gameObject.SetActive(true);

            }

            gm.roomManager.GetComponent<Animator>().Play("Bridal");

            transform.GetChild(0).gameObject.SetActive(false);

            gm.TutorialNavi.gameObject.SetActive(false);
            
        }
        else
        {
            gm.playerObject.SetActive(true);
			
            if(gm.playerMovement.saveStationNum == 2)
            {
                gm.roomManager.GetComponent<Animator>().Play("Altar Room");
            }
            else if(gm.playerMovement.saveStationNum == 3)
            {
                gm.roomManager.GetComponent<Animator>().Play("Kitchen");
            }
            else if(gm.playerMovement.saveStationNum == 1)
            {
                 gm.roomManager.GetComponent<Animator>().Play("Bridal");
                
            }
            else
            {
                Application.LoadLevel(Application.loadedLevel);
            }    
            
            gm.playerObject.GetComponent<Transform>().position = gm.playerMovement.savedLocation.position;
            gm.ghostMain.GetComponent<Transform>().position = gm.carrotMain.inactiveLocation.position;
            gm.player.isDead = false;
            

            int child = GameObject.Find("DeathCanvas").transform.childCount;
            for (int i = 0; i < child; i++)
            {
                GameObject.Find("DeathCanvas").transform.GetChild(i).gameObject.SetActive(false);
            }
            //Application.LoadLevel(Application.loadedLevel);

        }

    }
	
	void UpdateAudio(int index)
	{
		switch(index)
		{
			case 1:
				gm.audioManager.ForcePlayAudio("button click");
				break;
		}
	}
}
