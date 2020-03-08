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
    // Start is called before the first frame update

    private void Awake()
    {
        gm = GameManager.Instance;
        
    }

    void Start()
    {
        
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        ChanceToTeleportGhost(col);
    }

    private void ChanceToTeleportGhost(Collider2D collider)
    {
        if (collider.gameObject == gm.playerObject)
        {
            if (!isDisabled)
            {
                gm.ghostManager.currentMirror = transform;
                Debug.Log("Whoosh! You walked past the spot!");

             
                    if (!ghostTeleporting)
                    {
                        StartCoroutine(CountDown(countDownTime));
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

    }

    IEnumerator CountDown(int countDownTime)
    {
        ghostTeleporting = true;

        for (int i = countDownTime; i > 0; i--)
        {
            Debug.Log("Warning! Ghost will teleport to the ROOM in " + i + " second(s)!");
            yield return new WaitForSeconds(1);
        }
        if(!isDisabled)
        {
            gm.ghostManager.currentDoor = door[doorNumber].transform;
            gm.carrotMain.TeleportTrigger();
        }
        
       // isDisabled = true;
        ghostTeleporting = false;
    }


}
