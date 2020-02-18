using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine;

public class Hidable : MonoBehaviour
{

    public float darkness;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Hide()
    {
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<Renderer>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hidden = true;
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
        GameObject.Find("Global Light 2D").GetComponent<Light2D>().intensity = darkness;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Unhide()
    {
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<Renderer>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hidden = false;
       
        if (GameObject.Find("Global Light 2D"))
        {
            GameObject.Find("Global Light 2D").GetComponent<Light2D>().intensity = 0.15f;
        }

        transform.GetChild(0).gameObject.SetActive(false);
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
