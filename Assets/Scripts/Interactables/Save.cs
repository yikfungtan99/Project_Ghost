using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : Interactable
{
    // Start is called before the first frame update
    public Transform spawnPoint;

    private void Start()
    {
        spawnPoint = this.gameObject.GetComponentInChildren<Transform>();
    }
    public override void Interact()
    {

        saveGame();
        gm.playerObject.GetComponent<Player_Movement>().save(spawnPoint);
    }


    public void saveGame()
    {
        Debug.LogWarning("GAME SAVED");
        
    }
    // Upate is called once per frame
    
}
