using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Triggers : MonoBehaviour
{

    public bool isEnabled = false;
    public bool isCanceler = false;
    public bool final = false;

    public bool promptSleep = false;

    public bool isDisabled = false;

    public bool ready = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (promptSleep)
        {
            if (collision.CompareTag("Player"))
            {
                promptSleep = false;
                UpdateMonologue(1);
            }
        }
        else
        {
            if (!final)
            {

                if (isCanceler)
                {

                    if (GameManager.Instance.TutorialNavi.gameObject)
                    {

                        GameManager.Instance.TutorialNavi.gameObject.SetActive(false);
                        GameManager.Instance.playerInventory.firstTime = false;

                    }

                }
                else
                {
                    if (isEnabled)
                    {
                        if (collision.GetComponent<Player>())
                        {
                            if (GameManager.Instance.tutorialSleep)
                            {

                                GameManager.Instance.TutorialGhostTrigger(true);

                                GameManager.Instance.TutorialNavi.gameObject.SetActive(true);
                                GameManager.Instance.TutorialNavi.UpdateBoard(7);

                            }
                            else
                            {
                                GameManager.Instance.TutorialNavi.UpdateBoard(1);
                            }

                            isEnabled = false;

                        }
                    }
                }


            }
            else
            {

                if (collision.CompareTag("Enemy"))
                {

                    if (ready)
                    {

                        if (!GameManager.Instance.player.isDead)
                        {

                            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);

                        }

                        //play door sound here
                        StartCoroutine(UpdateMonologueAfterWaitSeconds(2, 4));
                    }

                }

            }

        }

    }

    IEnumerator UpdateMonologueAfterWaitSeconds(int displayIndex, int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            yield return new WaitForSeconds(1);
        }
        UpdateMonologue(displayIndex);
    }

    void UpdateMonologue(int displayIndex)
    {
        switch(displayIndex)
        {
            case 1: //! prompt player to sleep
                GameManager.Instance.monologueManager.DisplaySentence(34);
                break;
            case 2: //! prompt player to leave bedroom after sleep
                GameManager.Instance.monologueManager.DisplaySentence(36);
                break;
        }
    }

    void UpdateAudio(int index)
    {
        switch(index)
        {
            case 1: //! play some eerie/unknown door sound things (to hint at ghost presence)

                break;
        }
    }
}
