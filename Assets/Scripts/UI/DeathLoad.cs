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

        ghostInitPos = gm.ghostMain.transform.position;

    }

    [System.Obsolete]
    public void Restart()
    {

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

            Application.LoadLevel(Application.loadedLevel);

        }

    }

}
