using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine;

public class Hidable : Interactable
{
    private GameObject player;
    public float darkness;

    // Start is called before the first frame update
    public void Start()
    {
        player = gm.playerObject;
    }

    public override void Interact()
    {
        base.Interact();

        Hide();

    }

    public void Hide()
    {
        //player.GetComponent<Player_Movement>().enabled = false;
        player.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
        gm.player.hidden = true;
        player.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
        gm.GlobalLight.intensity = darkness;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Unhide()
    {
        player.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
        player.GetComponent<Player>().hidden = false;
       
        if (gm.GlobalLight)
        {
            gm.GlobalLight.intensity = 0.15f;
        }

        transform.GetChild(0).gameObject.SetActive(false);
    }
}
