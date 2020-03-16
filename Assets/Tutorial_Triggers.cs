using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Triggers : MonoBehaviour
{

    public bool isEnabled = false;
    public bool isCanceler = false;
    public bool final = false;

    public bool isDisabled = false;

    public bool ready = false;

    private void OnTriggerEnter2D(Collider2D collision)
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
                        
                }

            }

        }



    }

}
