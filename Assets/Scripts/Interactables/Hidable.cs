using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine;

public class Hidable : Interactable
{
    private GameObject player;
    public float darkness;
    private float initDark;
    private float initY;
    public float cdTime;
    private float cdCount;
    private bool isUsable=true;

    // Start is called before the first frame update
    public void Start()
    {
        player = gm.playerObject;
        initDark = gm.GlobalLight.intensity;
        transform.GetChild(0).GetComponent<Light2D>().intensity = initDark;
        cdCount = 0;
        cdTime = 2;
    }

    public override void Update()
    {
        base.Update();

        if(!isUsable)
        {
            cdCount += Time.deltaTime;

            if (cdCount >= cdTime)
            {
                cdCount = 0;
                isUsable = true;
            }
        }
    }

    public override void UpdateCursor()
    {
        gm.mouseControl.changeCursor("hide");
    }

    public override void Interact()
    {
        if(isUsable==true)
        {
            base.Interact();

            Hide();
        }
        

    }

    public void Hide()
    {
        UpdateAudio(1);
        UpdateAudio(2);
        initY = player.transform.position.y;
        player.GetComponent<Player_Movement>().enabled = false;
        player.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
        gm.player.hidden = true;
        player.layer = LayerMask.NameToLayer("HideLayer");
        player.transform.position = new Vector2(transform.position.x, transform.position.y);
        player.GetComponent<Player>().curHidable = transform.gameObject;
        gm.GlobalLight.intensity = 0;

        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Unhide()
    {
        isUsable = false;
        UpdateAudio(3);
        UpdateAudio(2);
        Debug.Log(initY);

        player.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
        player.GetComponent<Player>().hidden = false;
        player.layer = LayerMask.NameToLayer("Player");
        player.transform.position = new Vector2(transform.position.x, initY);

        player.GetComponent<Player>().curHidable = null;

        gm.GlobalLight.intensity = initDark;

        transform.GetChild(0).gameObject.SetActive(false);
    }

    public override void UpdateAudio(int index)
    {
        switch(index)
        {
            case 1:
                gm.audioManager.FadeInAudio("heart beating", 0f);
                break;
            case 2:
                gm.audioManager.PlayAudio("hiding door");
                break;
            case 3:
                gm.audioManager.ForceStopAudio("heart beating");
                break;
        }
    }
}
