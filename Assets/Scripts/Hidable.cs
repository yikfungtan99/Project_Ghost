using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Hide()
    {
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<Renderer>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hidden = true;
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
