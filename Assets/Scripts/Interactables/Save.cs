using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : Interactable
{
    // Start is called before the first frame update
    public Transform spawnPoint;
    public int saveStationNumber;
    private void Start()
    {
        spawnPoint = this.gameObject.transform.Find("SaveSpawn");
    }

    public override void Interact()
    {
        base.Interact();

        SaveGame();
    }

    void SaveGame()
    {
        if(itemName == "talisman")
        {
            itemName = null;
            gm.playerObject.GetComponent<Player_Movement>().LoadSave(spawnPoint, saveStationNumber, true);
        }
        else
        {
            gm.playerObject.GetComponent<Player_Movement>().LoadSave(spawnPoint, saveStationNumber, false);
        }
        //Debug.Log("GAME SAVED");
    }
}
