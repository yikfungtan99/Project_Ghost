using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected GameManager gm;

    public bool inRange = false;
    public bool interactable = true;
    public bool isSeen = false;
    public bool itemGiver = false;

    public string itemName;
    public int charges = 0;

    virtual public void Awake()
    {
        gm = GameManager.Instance;

    }

    public virtual void Interact()
    {
        if (CheckIfHiding())
        {
            return;
        }

        if (itemGiver)
        {
            if(itemName != null)
            {

                if(charges  == 0)
                {
                    Debug.LogWarning(gameObject.name + ": Charges is 0!!!");
                }

                GiveItem();

            }
            else
            {

                Debug.LogError(gameObject.name + ": ItemGiver item name empty!");

            }
            

        }
    }

    public virtual void UpdateCursor()
    {

        gm.mouseControl.changeCursor("interact");

    }

    protected void GiveItem()
    {

        if (charges > 0)
        {

            if (gm)
            {

                gm.inventory.ObtainItem(itemName);

            }
            else
            {

                Debug.LogError(gameObject.name + "GameManager Not Found");

            }
           
            charges -= 1;


            UpdateMonologue(-1, itemName);
        }

    }

    protected void CheckHoldSlot()
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
                    UpdateMonologue(-1, "");
                }

            }
            else
            {

                UpdateMonologue(-1, "");

            }
        }

    }

    public virtual void UpdateMonologue(int displayIndex, string itemName)
    {
        if (itemName == "talisman")
        {
            gm.monologueManager.DisplaySentence(8);
        }
        else if(itemName != "")
        {
            //! this is for all item givers (where the item come from inside a container in-game)
            gm.monologueManager.DisplayPickUpSentence(itemName, true);
        }
    }

    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (GetComponentInChildren<ParticleSystem>(true))
            {
                GetComponentInChildren<ParticleSystem>(true).gameObject.SetActive(isSeen);
            }
        }
    }

    private void LateUpdate()
    {
        inRange = false;
        isSeen = false;
    }

    private bool CheckIfHiding()
    {
        if(GameManager.Instance.player.hidden)
        {
            return true;
        }
        return false;
    }
}
