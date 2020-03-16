using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    private RoomManager rm;
    private GameObject player;

    public bool isLocked = false;
    public bool isClosed = true;

    [Header("Orientation")]

    public bool LeftRight = false;
    public bool UpDown = false;

    public GameObject linkedDoor;
    private Transform doorSpawnPoint;

    private void Start()
    {
        if (gm)
        {
            rm = gm.roomManager;
        }
        else
        {
            Debug.Log("Cant find gameManager");
        }

        player = gm.playerObject;

        if(linkedDoor == null)
        {
            Debug.LogError(gameObject.name + " have missing door connection!!!");
        }
        else
        {
            doorSpawnPoint = linkedDoor.transform.GetChild(0);
        }
    }

    public override void UpdateCursor()
    {
        gm.mouseControl.changeCursor("door");
    }

    public void TryToUnlock()
    {
        //! the repitition is a template only,hoping that there will be custom monologue for each door
        if(GatewayIsLocked(gm.doorHorizontalDiningToHall4))
        {
            UpdateAudio(2);
            UpdateMonologue(1, "");
            Debug.Log("Dining to Hall4");
        }
        if(GatewayIsLocked(gm.doorVerticalKitchenToToilet))
        {
            UpdateAudio(2);
            UpdateMonologue(1, "");
            Debug.Log("Kitchen to Toilet");
        }
        if(GatewayIsLocked(gm.doorVerticalLivingToHall2))
        {
            UpdateAudio(2);
            UpdateMonologue(1, "");
            Debug.Log("Living to Hall2");
        }
        if (GatewayIsLocked(gm.doorVerticalMainToStorage))
        {
            UpdateAudio(2);
            UpdateMonologue(1, "");
            Debug.Log("Main to Storage");
        }
        if (GatewayIsLocked(gm.doorHorizontalOutsideToMain))
        {
            Debug.Log("aa");
            gm.TutorialNavi.GetComponent<Animator>().SetTrigger("Bag");
            gm.playerInventory.firstTime = true;

            if (CheckHoldSlot())
            {
                //gm.TutorialNavi.GetComponent<Animator>().SetTrigger("Next");
                SetIsLockedOnDoor(this.transform.root.gameObject, false);

                UpdateAudio(3);
                UpdateMonologue(2, "");
            }
            else
            {
                UpdateAudio(2);
                UpdateMonologue(1, "");
                Debug.Log("Outside to Main");
            }
        }

    }

    public void Open()
    {
        isClosed = false;

        if (gm.playerInventory.firstTime)
        {
            gm.TutorialNavi.GetComponent<Animator>().SetTrigger("Bag1");
        }

        if (LeftRight)
        {
            if (player.transform.position.x > doorSpawnPoint.position.x)
            {
                rm.SwitchRoom("Left", transform);
                player.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                rm.SwitchRoom("Right", transform);
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else if (UpDown)
        {
            if (player.transform.position.y > doorSpawnPoint.position.y)
            {
                rm.SwitchRoom("Down", transform);
            }
            else
            {
                rm.SwitchRoom("Up", transform);
            }
        }

        player.transform.position = doorSpawnPoint.position;

        //! this checks doors to change ambience when needed
        if(transform == gm.doorVerticalHall1ToBridal.transform.GetChild(1).transform)
        {
            UpdateAudio(5);
        }
        else
        {
            UpdateAudio(4);
        }

        StartCoroutine(AutoClose(1.0f));
    }

    public void Closed()
    {

        isClosed = true;

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //! For left right doors
        if(col.gameObject == gm.playerObject)
        {
            Interact();
        }

        if (col.gameObject.CompareTag("Enemy"))
        {

            if (col.gameObject.GetComponent<CarrotMain>())
            {
                if (col.gameObject.GetComponent<CarrotMain>().canChangeRoom)
                {

                    EnemyOpen(col.gameObject);

                }
                
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            if (collision.gameObject.GetComponent<CarrotMain>())
            {
                if (collision.gameObject.GetComponent<CarrotMain>().canChangeRoom)
                {

                    EnemyOpen(collision.gameObject);

                }

            }
        }

    }

    public override void Interact()
    {
        base.Interact();

        if (isLocked)
        {
            TryToUnlock();
        }
        else if(isClosed)
        {
            UpdateAudio(1);
            Open();
        }
    }

    public void EnemyOpen(GameObject enemy)
    {

        enemy.transform.position = doorSpawnPoint.position;
        enemy.GetComponent<CarrotMain>().canChangeRoom = false;

    }

    IEnumerator AutoClose(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Closed();
    }

    public void SetIsLockedOnDoor(GameObject door, bool statement)
    {
        for (int i = 0; i < door.transform.childCount; i++)
        {
            door.transform.GetChild(i).GetComponent<Door>().isLocked = statement;
        }

        if(statement == false)
        {

            Unlock();

        }

    }

    private void Unlock()
    {

        Debug.Log("Its Unlocked!");

    }

    private bool GatewayIsLocked(GameObject door)
    {
        bool isDoorCurrentlyLocked = false;
        bool isDoorInRange = false;

        isDoorCurrentlyLocked = door.transform.GetChild(0).GetComponent<Door>().isLocked;
        if(gm.playerInteractable.publicMouseHit.collider.transform.parent.gameObject == door)
        {
            if(gm.playerInteractable.publicMouseHit.collider.GetComponent<Door>().inRange)
            {
                isDoorInRange = inRange;
            }
        }

        if(isDoorCurrentlyLocked && isDoorInRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void UpdateMonologue(int displayIndex, string itemName)
    {
        switch(displayIndex)
        {
            case 1:
                gm.monologueManager.DisplaySentence(22);
                break;
            case 2:
                gm.monologueManager.DisplaySentence(23);
                break;
        }
    }

    public override void UpdateAudio(int index)
    {
        switch(index)
        {
            case 1:
                gm.audioManager.ForcePlayAudio("normal door");
                break;
            case 2:
                gm.audioManager.ForcePlayAudio("locked door");
                break;
            case 3:
                gm.audioManager.PlayAudio("unlock door");
                break;
            case 4:
                //! first force stop all other potential ambience playing
                gm.audioManager.ForceStopAudio("disquiet ambience");

                gm.audioManager.FadeInAudio("normal ambience", 0);
                break;
            case 5:
                //! first force stop all other potential ambience playing
                gm.audioManager.ForceStopAudio("normal ambience");

                gm.audioManager.FadeInAudio("disquiet ambience", 0);
                break;
        }
    }
}
