using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected GameManager gm;

    public bool inRange = false;
    public bool interactable = true;
    public bool isSeen = false;

    public string itemName;
    public int charges = 0;

    string[] stringtags = new string[] { "Door", "Prop", "Dropped_Item","Hiding_Spot", "Candle", "Safe_Bowl", "Death_Bowl", "Note"};

    private void Start()
    {
        gm = GameManager.Instance;
    }

    public virtual void Interact()
    {
        if (CheckIfHiding())
        {
            return;
        }
    }

    protected void GiveItem()
    {

        if (charges > 0)
        {
            gm.playerInventory.inventory.GetComponent<Inventory>().ObtainItem(itemName);
            charges -= 1;

            UpdateMonologue();
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
                    UpdateMonologue();
                }

            }
            else
            {

                UpdateMonologue();

            }
        }

    }

    protected void UpdateMonologue()
    {
        if (itemName == "talisman")
        {
            GameManager.Instance.GetComponent<MonologueManager>().DisplaySentence(8);
        }
    }

    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (GetComponentInChildren<Interactable_Glow>(true))
            {
                GetComponentInChildren<Interactable_Glow>(true).gameObject.SetActive(isSeen);
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
