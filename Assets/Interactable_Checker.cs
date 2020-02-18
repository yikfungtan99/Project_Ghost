using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Checker : MonoBehaviour
{

    public string itemName;

    // Start is called before the first frame update
    public void CheckHoldPanel()
    {

        if (GameObject.Find("Player"))
        {

            Transform holdPanel = GameObject.Find("Player").transform.GetChild(2).GetChild(1);

            if (holdPanel.childCount != 0)
            {

                if (holdPanel.GetChild(0).GetComponent<Item_Inventory>().itemName == itemName)
                {

                    GameObject.Find("Win").transform.GetChild(0).gameObject.SetActive(true);
                    Time.timeScale = 0;

                }
                else
                {
                    UpdateMonologue();
                }

            }
            else
            {

                UpdateMonologue();

            }
        }

    }

    void UpdateMonologue()
    {
        GameObject.Find("MonologueManager").GetComponent<MonologueManager>().DisplaySentence(2);
    }
}
