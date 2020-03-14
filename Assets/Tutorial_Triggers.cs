using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Triggers : MonoBehaviour
{

    public bool isEnabled = false;
    public bool isCanceler = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCanceler) {

            if(GameManager.Instance.TutorialNavi.gameObject)
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

                        GameManager.Instance.TutorialNavi.GetComponent<Animator>().SetTrigger("Hide");

                    }
                    else
                    {
                        GameManager.Instance.TutorialNavi.GetComponent<Animator>().SetTrigger("Door");

                    }

                    Destroy(this.gameObject);

                }
            }
        }
       
       

    }

}
