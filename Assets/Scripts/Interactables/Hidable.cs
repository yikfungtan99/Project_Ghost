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

    // Start is called before the first frame update
    public void Start()
    {
        player = gm.playerObject;
        initDark = gm.GlobalLight.intensity;
        transform.GetChild(0).GetComponent<Light2D>().intensity = initDark;
    }

    public override void Interact()
    {
        base.Interact();

        Hide();

    }

    public void Hide()
    {

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

        Debug.Log(initY);

        player.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
        player.GetComponent<Player>().hidden = false;
        player.layer = LayerMask.NameToLayer("Player");
        player.transform.position = new Vector2(transform.position.x, initY);

        player.GetComponent<Player>().curHidable = null;

        gm.GlobalLight.intensity = initDark;

        transform.GetChild(0).gameObject.SetActive(false);
    }
}
