using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    protected GameManager gm;
    public bool isDisabled = false;
    public bool ghostTeleporting = false;
    public int countDownTime;
    public Transform[] door;
    public int doorNumber;
    private float randomChance;
    private float chance = 0;
    public float cdTime = 1;
    
    public bool disableAutoRecover;

    [HideInInspector]
    public bool canAutoRecover;

    // Start is called before the first frame update

    private void Awake()
    {
        gm = GameManager.Instance;
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        
        if(col.gameObject == gm.playerObject)
        {
            if (this.CompareTag("StorageRoomTrigger"))
            {
                ChanceToTeleportGhost(false, true);
            }
            else
            {
                ChanceToTeleportGhost(false, false);
            }
            
        }
       
    }

    public void ChanceToTeleportGhost(bool ignoreDisable, bool hundredPercent)
    {

        if (!isDisabled)
        {
            

            gm.ghostManager.currentMirror = transform;
            Debug.Log("Whoosh! You walked past the spot!");

             
                if (!ghostTeleporting)
                {
                    StartCoroutine(CountDownTrigger(countDownTime, hundredPercent));
                    

                }
                else
                {
                    Debug.Log("Ghost is already teleporting!");
                }

        }
        else
        {
            Debug.Log("Spot disable");
        }


    }

    void Warning()
    {
        if (gm.playerObject.transform.position.x > door[doorNumber].position.x)
        {
            Debug.Log("LEFT!");
            gm.playerObject.GetComponent<Player>().WarningLeft.SetActive(true);
            

        }
        else
        {
            Debug.Log("RIGHT!");
            gm.playerObject.GetComponent<Player>().WarningRight.SetActive(true);
        }

        StartCoroutine(WarningDisable());
    }

    IEnumerator WarningDisable()
    {
        yield return new WaitForSeconds(2);
        gm.playerObject.GetComponent<Player>().WarningLeft.SetActive(false);
        gm.playerObject.GetComponent<Player>().WarningRight.SetActive(false);

    }


    IEnumerator CountDownTrigger(int countDownTime, bool hundredPercent)
    {
        
        ghostTeleporting = true;
        randomChance = Random.Range(0, 2);
        if(chance==randomChance || hundredPercent)
        {
            UpdateAudio(1);
            Warning();
        }

        for (int i = countDownTime; i > 0; i--)
        {
            Debug.Log("Warning! Ghost will teleport to the ROOM in " + i + " second(s)!");
            yield return new WaitForSeconds(1);
        }

        gm.ghostManager.currentDoor = door[doorNumber].transform;

        if (chance == randomChance || hundredPercent)
        {
            
            gm.carrotMain.TeleportTrigger();
            //Warning();

        }
        else
        {

            Debug.Log("No Tele");
                

        }

        
        isDisabled = true;
        ghostTeleporting = false;

        if (canAutoRecover && !disableAutoRecover)
        {
            StartCoroutine(triggerAutoRecover(cdTime));
        }
    }

    IEnumerator triggerAutoRecover(float recoverCD)
    {

        yield return new WaitForSeconds(recoverCD);
        isDisabled = false;
        Debug.Log("Recovered");

    }

    void UpdateAudio(int index)
    {
        switch(index)
        {
            case 1:
                gm.audioManager.PlayAudio("ghost trigger");
                break;
        }
    }
}
